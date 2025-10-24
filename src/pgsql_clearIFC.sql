-- Сначала очищаем
UPDATE parameters_str
SET comment = NULL
WHERE idparamdef = (
    SELECT idparamdef 
    FROM paramdefs 
    WHERE UPPER(name) = UPPER('CLASS_IFC4')
);

