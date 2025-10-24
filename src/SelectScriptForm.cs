using System;
using System.Collections.Generic;
using System.Windows.Forms;

public partial class SelectScriptForm : Form
{
    public string SelectedResource { get; private set; }

    public SelectScriptForm(List<string> availableScripts)
    {
        InitializeComponent();
        comboBoxScripts.Items.AddRange(availableScripts.ToArray());
        if (availableScripts.Count > 0)
            comboBoxScripts.SelectedIndex = 0;
    }

    private void InitializeComponent()
    {
        this.comboBoxScripts = new System.Windows.Forms.ComboBox();
        this.btnOK = new System.Windows.Forms.Button();
        this.btnCancel = new System.Windows.Forms.Button();
        this.label1 = new System.Windows.Forms.Label();
        this.SuspendLayout();

        // label1
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(12, 15);
        this.label1.Text = "Выберите SQL-скрипт:";

        // comboBoxScripts
        this.comboBoxScripts.DropDownStyle = ComboBoxStyle.DropDownList;
        this.comboBoxScripts.Location = new System.Drawing.Point(15, 40);
        this.comboBoxScripts.Size = new System.Drawing.Size(360, 21);

        // btnOK
        this.btnOK.Location = new System.Drawing.Point(215, 80);
        this.btnOK.Size = new System.Drawing.Size(75, 23);
        this.btnOK.Text = "ОК";
        this.btnOK.DialogResult = DialogResult.OK;
        this.btnOK.Click += (s, e) => {
            SelectedResource = comboBoxScripts.SelectedItem?.ToString();
        };

        // btnCancel
        this.btnCancel.Location = new System.Drawing.Point(295, 80);
        this.btnCancel.Size = new System.Drawing.Size(75, 23);
        this.btnCancel.Text = "Отмена";
        this.btnCancel.DialogResult = DialogResult.Cancel;

        // Form
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(384, 115);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.comboBoxScripts);
        this.Controls.Add(this.btnOK);
        this.Controls.Add(this.btnCancel);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.StartPosition = FormStartPosition.CenterParent;
        this.Text = "Выбор скрипта";
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.ComboBox comboBoxScripts;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Label label1;
}