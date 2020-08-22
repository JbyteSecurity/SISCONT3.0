USE SISCONT
GO

SELECT
	--CREDITO/DEBITO FILCA IGV
	SUM(rv.IGV) - SUM(rc.IGV) AS 'Impuesto Resultante'
FROM tblRegistroVentas rv
JOIN tblRegistroCompras rc ON rc.rucEmpresa = rv.rucEmpresa
WHERE rc.rucEmpresa = '20448484816'
GROUP BY rc.Mes

SELECT
-- EGRESOS
	Mes,
	SUM(BaseImponible) AS 'Base Imponible',
	SUM(IGV) AS EIGV,
	SUM(NoGravada) AS 'NO Gravada',
	SUM(ImporteTotal) AS 'Importe Total'
FROM tblRegistroCompras
WHERE rucEmpresa = '20448484816'
GROUP BY Mes


SELECT
	--INGRESOS
	Mes,
	sum(ValorExportacion) AS Exportación,
	SUM(BaseImponible) AS Gravadas,
	SUM(ImporteTotalExonerada) AS Exonerada,
	SUM(ImporteTotalInafecta) AS Inafecta,
	SUM(IGV) AS IIGV,
	SUM(ImporteTotal) AS 'Importe Total'
FROM tblRegistroVentas
WHERE rucEmpresa = '20448484816'
GROUP BY Mes

SELECT * FROM tblRegistroVentas
SELECT * FROM tblRegistroCompras