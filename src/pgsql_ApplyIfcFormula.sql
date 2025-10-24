DO $$
DECLARE
    v_idparamdef INTEGER;
BEGIN
    SELECT idparamdef INTO v_idparamdef
    FROM paramdefs
    WHERE UPPER(name) = UPPER('CLASS_IFC4')
    LIMIT 1;

    IF v_idparamdef IS NULL THEN
        RAISE EXCEPTION 'Параметр CLASS_IFC4 не найден';
    END IF;

    UPDATE parameters_str
    SET comment = '{{FORMULA}}'
    WHERE idparamdef = v_idparamdef;

    RAISE NOTICE 'Обновлено % записей', 
        (SELECT COUNT(*) FROM parameters_str WHERE idparamdef = v_idparamdef);
END $$;