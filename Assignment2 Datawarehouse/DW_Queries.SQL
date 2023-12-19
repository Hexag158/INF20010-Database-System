-----------------------------8.A--------------------------
select to_char(DDT.datevalue, 'DAY') as "WEEKDAY", 
sum(DSL.qty * DSL.saleprice) as "Total Sale"
from DWSALE DSL
inner join DWDATE DDT on DSL.sale_dwdateid = DDT.datekey
group by to_char(DDT.datevalue, 'DAY')
order by SUM(DSL.QTY * DSL.SALEPRICE) desc;
/
-----------------------------8.B--------------------------
SELECT DWCUST.CUSTCATNAME AS "CUSTCATNAME",
SUM(DWSALE.qty * DWSALE.saleprice) as "TOTALSALES"
FROM DWSALE
INNER JOIN DWCUST ON DWSALE.DWCUSTID = DWCUST.DWCUSTID
GROUP BY CUSTCATNAME
ORDER BY SUM(DWSALE.QTY * DWSALE.SALEPRICE) ASC;
/
-----------------------------8.C--------------------------
SELECT DWPROD.PRODMANUNAME AS "PRODMANUNAME",
SUM(DWSALE.QTY) as "TOTAL QTY SOLD"
FROM DWSALE
INNER JOIN DWPROD ON DWSALE.DWPRODID = DWPROD.DWPRODID
GROUP BY PRODMANUNAME
ORDER BY SUM(DWSALE.QTY) DESC;
/
-----------------------------8.D--------------------------
SELECT DWC.DWCUSTID AS "DWCUSTID",
DWC.FIRSTNAME AS "FIRSTNAME",
DWC.SURNAME AS "SURNAME",
SUM(DWSALE.qty * DWSALE.saleprice) as "TOTALSALES"
FROM DWSALE
INNER JOIN DWCUST DWC ON DWSALE.DWCUSTID = DWC.DWCUSTID
GROUP BY DWC.DWCUSTID,DWC.FIRSTNAME,DWC.SURNAME
ORDER BY SUM(DWSALE.QTY * DWSALE.SALEPRICE) DESC
FETCH FIRST 10 ROWS ONLY;
/
-----------------------------8.E--------------------------
SELECT DWP.DWPRODID AS "DWPRODID",
DWP.PRODNAME AS "PRODNAME",
SUM(DWSALE.qty) as "TOTALSALES"
FROM DWSALE
INNER JOIN DWPROD DWP ON DWSALE.DWPRODID = DWP.DWPRODID
GROUP BY DWP.DWPRODID, DWP.PRODNAME
ORDER BY SUM(DWSALE.QTY) ASC
FETCH FIRST 10 ROWS ONLY;
/
-----------------------------8.F--------------------------
select STATE, CITY, sum(dws.qty * dws.saleprice) as "Total Sale"
from dwcust dwc
inner join dwsale dws on dwc.dwcustid = dws.dwcustid
group by dwc.CITY, dwc.STATE
order by dwc.STATE asc;
/