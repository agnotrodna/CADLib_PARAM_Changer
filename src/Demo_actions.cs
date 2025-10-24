using CADLib;
using CADLibControls;
using CADLibKernel;
using CdeLib;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using LightweightDataAccess;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static IFC_PLUGIN.CADLib_data;

namespace IFC_PLUGIN
{
    partial class Demo_Form
    {
        private void _DReportExchangeProgress(EDataExchangeProgress p, object value) { }
        private Stopwatch timer = new Stopwatch();

        


        // === Внутренние методы ===

        private void ClearPARAM_Internal()
        {
            string fullResourceName = "IFC_PLUGIN.pgsql_clearIFC.sql";
            var assembly = Assembly.GetExecutingAssembly();

            if (!assembly.GetManifestResourceNames().Contains(fullResourceName))
                throw new Exception($"SQL-ресурс не найден: {fullResourceName}");

            using (var stream = assembly.GetManifestResourceStream(fullResourceName))
            using (var reader = new StreamReader(stream))
            {
                string sql = reader.ReadToEnd();
                string sqlTempFile = Path.Combine(dir_cadlib_output, "clear_comment.sql");
                if (File.Exists(sqlTempFile)) File.Delete(sqlTempFile);
                File.WriteAllText(sqlTempFile, sql, Encoding.UTF8);
                RunSQL(sqlTempFile);
            }
        }

        private void ApplyIfcFormula_Internal()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string outputFilePath = Path.Combine(appData, "CADLibPlugin", "ClassIFC", "output.txt");

            if (!File.Exists(outputFilePath))
                throw new Exception("Файл output.txt не найден.");

            string formula = File.ReadAllText(outputFilePath, Encoding.UTF8).Replace("'", "''");

            string fullResourceName = "IFC_PLUGIN.pgsql_ApplyIfcFormula.sql";
            var assembly = Assembly.GetExecutingAssembly();

            if (!assembly.GetManifestResourceNames().Contains(fullResourceName))
                throw new Exception($"SQL-ресурс не найден: {fullResourceName}");

            using (var stream = assembly.GetManifestResourceStream(fullResourceName))
            using (var reader = new StreamReader(stream))
            {
                string sqlTemplate = reader.ReadToEnd();
                string finalSql = sqlTemplate.Replace("{{FORMULA}}", formula);
                string sqlTempFile = Path.Combine(dir_cadlib_output, "apply_ifc_formula.sql");
                if (File.Exists(sqlTempFile)) File.Delete(sqlTempFile);
                File.WriteAllText(sqlTempFile, finalSql, Encoding.UTF8);
                RunSQL(sqlTempFile);
            }
        }

        private async Task UpdateParametersAsync_NoUI()
        {
            await Task.Run(() =>
            {
                DbTransactionInfo txn = null;
                try
                {
                    m_library.WithNativeConnection(conn =>
                    {
                        using (txn = conn.BeginTransaction())
                        {
                            m_library.UpdateCalculatedParameters(
                                txn,
                                "Objects",
                                "idObject",
                                "idParentObject",
                                m_library.Connection.TempTableName("tt_"),
                                false,
                                (key, value) => { } 
                            );
                            txn.Commit();
                        }
                    });
                }
                catch (Exception ex)
                {
                    txn?.Rollback();
                    throw;
                }
            });
        }

        private void RunSQL(string sql_path)
        {
            if (!File.Exists(sql_path))
                throw new Exception("SQL-файл не найден: " + sql_path);

            m_library.RunSqlScripts(
                new DMakeDbCommandInfo(m_library.Connection.Command),
                dir_cadlib_output,
                new[] { sql_path },
                _DReportExchangeProgress
            );
        }

        
        private async void MenuItem_RunAllInOrder_Click(object sender, EventArgs e)
        {
            Form progressForm = null;
            Label statusLabel = null;
            ProgressBar progressBar = null;

            try
            {
              
                string exePath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Plugins",
                    "IFC_Plugin",
                    "pythonProc.exe"
                );

                if (!File.Exists(exePath))
                {
                    MessageBox.Show($"Файл не найден:\n{exePath}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

              
                using (var process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo
                    {
                        FileName = exePath,
                        UseShellExecute = false,
                        CreateNoWindow = false
                    };
                    process.Start();
                    await Task.Run(() => process.WaitForExit());
                }

             
                progressForm = new Form
                {
                    Size = new System.Drawing.Size(470, 150),
                    StartPosition = FormStartPosition.CenterParent,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false,
                    ShowInTaskbar = false,
                    ControlBox = false,
                    Text = "Выполнение операций..."
                };

                statusLabel = new Label
                {
                    Text = "Начало обработки...",
                    Location = new System.Drawing.Point(15, 15),
                    AutoSize = true,
                    MaximumSize = new System.Drawing.Size(420, 0)
                };

                progressBar = new ProgressBar
                {
                    Style = ProgressBarStyle.Continuous,
                    Location = new System.Drawing.Point(15, 45),
                    Size = new System.Drawing.Size(420, 23),
                    Minimum = 0,
                    Maximum = 100
                };

                progressForm.Controls.Add(statusLabel);
                progressForm.Controls.Add(progressBar);
                progressForm.Show();
                Application.DoEvents();

                progressForm.Invoke((MethodInvoker)delegate
                {
                    statusLabel.Text = "Применение формулы...";
                    progressBar.Value = 25;
                });
                ApplyIfcFormula_Internal();

                progressForm.Invoke((MethodInvoker)delegate
                {
                    statusLabel.Text = "Пересчёт параметров...";
                    progressBar.Value = 50;
                });
                await UpdateParametersAsync_NoUI();

                progressForm.Invoke((MethodInvoker)delegate
                {
                    statusLabel.Text = "Очистка параметров...";
                    progressBar.Value = 75;
                });
                ClearPARAM_Internal();


                progressForm.Invoke((MethodInvoker)delegate
                {
                    statusLabel.Text = "Завершение...";
                    progressBar.Value = 100;
                });

                await Task.Delay(300); 

                MessageBox.Show("Все операции успешно выполнены!", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выполнении последовательности:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (progressForm != null)
                {
                    progressForm.Close();
                    progressForm.Dispose();
                }
            }
        }

    }
}