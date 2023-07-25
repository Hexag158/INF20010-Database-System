--PART 1 ASSIGNMENT 1 
--103806269 - NGUYEN MINH NGHIA
create or replace PROCEDURE ADD_CUST_TO_DB (pcustid number, pcustname varchar2) AS
PCUSTID_OUTOF_RANGE EXCEPTION;
BEGIN
    IF pcustid < 1 or pcustid > 499 then
       RAISE PCUSTID_OUTOF_RANGE;   
    END IF;
    INSERT INTO CUSTOMER(custid, custname, sales_ytd, status) VALUES (pcustid, pcustname, 0, 'OK');  
EXCEPTION
    WHEN DUP_VAL_ON_INDEX THEN
        RAISE_APPLICATION_ERROR(-20013,'Duplicate customer ID');
    WHEN PCUSTID_OUTOF_RANGE THEN
        RAISE_APPLICATION_ERROR(-20027,'Customer ID out of range');
    WHEN OTHERS THEN
       RAISE_APPLICATION_ERROR(-20000 , SQLERRM);
END; 
/
create or replace PROCEDURE ADD_CUSTOMER_VIASQLDEV (pcustid NUMBER, pcustname varchar2) as

BEGIN
    DBMS_OUTPUT.PUT_LINE('--------------------------------------------');
    DBMS_OUTPUT.PUT_LINE('ADDING CUSTOMER: ' || pcustid || ' Name: '|| pcustname);
    ADD_CUST_TO_DB(pcustid, pcustname); 
    IF (SQL%ROWCOUNT>0) THEN
    DBMS_OUTPUT.PUT_LINE('Customer Added OK');
    COMMIT;
    END IF;    
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE(SQLERRM);
END;
/
create or replace FUNCTION DELETE_ALL_CUSTOMERS_FROM_DB RETURN number AS
vCount number;

BEGIN
    DELETE FROM CUSTOMER;
    vCount := SQL%ROWCOUNT;
    Return vCount;
    
EXCEPTION     
    WHEN OTHERS THEN     
        RAISE_APPLICATION_ERROR(-20000 , SQLERRM);
END;
/
create or replace PROCEDURE DELETE_ALL_CUSTOMERS_VIASQLDEV as
vResult number;
BEGIN
    DBMS_OUTPUT.PUT_LINE('--------------------------------------------');
    DBMS_OUTPUT.PUT_LINE('Deleting all Customer rows');
    vResult := DELETE_ALL_CUSTOMERS_FROM_DB();   
    DBMS_OUTPUT.PUT_LINE(vResult || ' rows deleted"');   
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE(SQLERRM);
END;
/
create or replace PROCEDURE ADD_PRODUCT_TO_DB (pprodid NUMBER, pprodname varchar2, pprice number) as
PROID_OUTOF_RANGE EXCEPTION;
PROPRICE_OUTOF_RANGE EXCEPTION;
BEGIN
    IF (pprice <0 OR pprice >999.99) THEN      
        RAISE PROPRICE_OUTOF_RANGE;     
    END IF;     
    IF (pprodid <1000 OR pprodid >2500) THEN     
        RAISE PROID_OUTOF_RANGE;     
    END IF;     
    INSERT INTO PRODUCT(PRODID, PRODNAME, SELLING_PRICE, SALES_YTD) 
    VALUES(pprodid, pprodname, pprice, 0);
EXCEPTION
    WHEN DUP_VAL_ON_INDEX THEN
        RAISE_APPLICATION_ERROR(-20033,'Duplicate product ID ');
    WHEN PROID_OUTOF_RANGE THEN
       RAISE_APPLICATION_ERROR(-20047,'Product ID out of range');
    WHEN PROPRICE_OUTOF_RANGE THEN
       RAISE_APPLICATION_ERROR(-20051,'Price out of range ');
    WHEN OTHERS THEN
       RAISE_APPLICATION_ERROR(-20000 , SQLERRM);
END;
/
create or replace PROCEDURE ADD_PRODUCT_VIASQLDEV (pprodid NUMBER, pprodname varchar2, pprice number) as
BEGIN
    DBMS_OUTPUT.PUT_LINE('--------------------------------------------');
    DBMS_OUTPUT.PUT_LINE('ADDING PRODUCT: ' || pprodid || ' Name: '|| pprodname || ' Price: ' || pprice);
    ADD_PRODUCT_TO_DB(pprodid ,pprodname ,pprice); 
    IF (SQL%ROWCOUNT>0) THEN
    DBMS_OUTPUT.PUT_LINE('Product Added OK');
    COMMIT;
    END IF;    
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE(SQLERRM);
END;
/
create or replace FUNCTION DELETE_ALL_PRODUCTS_FROM_DB  
                                        RETURN NUMBER AS 
vCount NUMBER; 
BEGIN     
    DELETE FROM PRODUCT;     
    vCount := SQL%ROWCOUNT;     
    RETURN vCount;     
EXCEPTION     
    WHEN OTHERS THEN     
        RAISE_APPLICATION_ERROR(-20000, SQLERRM); 
END;
/
create or replace procedure DELETE_ALL_PRODUCTS_VIASQLDEV as
vResult number;
begin
    DBMS_OUTPUT.PUT_LINE('--------------------------------------------');
    DBMS_OUTPUT.PUT_LINE('Deleting all product rows');     
    vResult := DELETE_ALL_PRODUCTS_FROM_DB();     
    DBMS_OUTPUT.PUT_LINE(vResult || ' rows deleted.');     
    COMMIT;     
    EXCEPTION     
        WHEN OTHERS THEN     
            DBMS_OUTPUT.PUT_LINE(SQLERRM); 
END;
/
create or replace FUNCTION GET_CUST_STRING_FROM_DB (pcustid number) 
return varchar2 AS
vname varchar2(100);
vstatus varchar2(10);
vsales number;
vreturn varchar2(200);
BEGIN
    SELECT CUSTNAME, STATUS, SALES_YTD 
    INTO vname, vstatus, vsales 
    FROM CUSTOMER WHERE CUSTID = pcustid;     
    vreturn:= 'Custid: '||pcustid||' Name: '||vname||' Status: '||vstatus||' SalesYTD:'||vsales;     
    RETURN vreturn;
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        RAISE_APPLICATION_ERROR(-20063,'Customer ID not found ');
    WHEN OTHERS THEN
       RAISE_APPLICATION_ERROR(-20000 , SQLERRM);
END;
/
create or replace PROCEDURE GET_CUST_STRING_VIASQLDEV  (pcustid NUMBER) as
vreturn varchar2(200);
BEGIN
    DBMS_OUTPUT.PUT_LINE('--------------------------------------------');
    DBMS_OUTPUT.PUT_LINE('Getting Details for CustId ' || pcustid );
    vreturn := GET_CUST_STRING_FROM_DB(pcustid);
    DBMS_OUTPUT.PUT_LINE(vreturn);
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE(SQLERRM);
END;
/
create or replace procedure UPD_CUST_SALESYTD_IN_DB (pcustid number,pamt number) AS
PAMT_OUTSIDE_RANGE EXCEPTION;
CUST_NOT_FOUND EXCEPTION;
BEGIN
    IF (pamt<-999.99 or pamt>999.99) THEN
        RAISE PAMT_OUTSIDE_RANGE;
    END IF;
    UPDATE CUSTOMER
    SET SALES_YTD = SALES_YTD + pamt
    WHERE CUSTID = pcustid;

    IF(SQL%NOTFOUND) THEN     
        RAISE CUST_NOT_FOUND;     
    END IF;

EXCEPTION
    WHEN CUST_NOT_FOUND THEN
       RAISE_APPLICATION_ERROR(-20073,'Customer ID not found ');
    WHEN PAMT_OUTSIDE_RANGE THEN
       RAISE_APPLICATION_ERROR(-20087,'Amount out of range ');
    WHEN OTHERS THEN
       RAISE_APPLICATION_ERROR(-20000 , SQLERRM);
END;  
/
create or replace PROCEDURE UPD_CUST_SALESYTD_VIASQLDEV (pcustid number,pamt number) as

BEGIN
    DBMS_OUTPUT.PUT_LINE('--------------------------------------------');
    DBMS_OUTPUT.PUT_LINE('Updating SalesYTD.' || pcustid ||' Amount: '|| pamt);
    UPD_CUST_SALESYTD_IN_DB(pcustid, pamt);     

    IF (SQL%FOUND) THEN         
        DBMS_OUTPUT.PUT_LINE('Update OK');         
        COMMIT;     
    END IF;
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE(SQLERRM);
END;
/
create or replace FUNCTION GET_PROD_STRING_FROM_DB (pprodid number) 
return varchar2 AS
vproname varchar2(20);
vproprice NUMBER;
vsales number;
vreturn varchar2(200);
BEGIN
    SELECT PRODNAME,SELLING_PRICE, SALES_YTD 
    INTO vproname, vproprice, vsales 
    FROM PRODUCT WHERE PRODID = pprodid;     
    vreturn := 'ProdID:'|| pprodid || ' Name:'|| vproname || ' Price:'|| vproprice || ' SalesYTD:' || vsales;     
    RETURN vreturn;
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        RAISE_APPLICATION_ERROR(-20093,'Product ID not found');
    WHEN OTHERS THEN
       RAISE_APPLICATION_ERROR(-20000 , SQLERRM);
END; 
/
create or replace PROCEDURE GET_PROD_STRING_VIASQLDEV(pprodid NUMBER) 
    AS vRESULT VARCHAR2(150); 
BEGIN   
    DBMS_OUTPUT.PUT_LINE('-------------------------------------');   
    DBMS_OUTPUT.PUT_LINE('Getting Details for Prod ID ' || pprodid);   
    vRESULT := GET_PROD_STRING_FROM_DB(pprodid);   
    DBMS_OUTPUT.PUT_LINE(vRESULT);   
EXCEPTION   
    WHEN OTHERS THEN   
        DBMS_OUTPUT.PUT_LINE(SQLERRM);
END;
/
create or replace procedure UPD_PROD_SALESYTD_IN_DB (pprodid number,pamt number) AS
PROD_OUTSIDE_RANGE EXCEPTION;
PRODID_NOT_FOUND EXCEPTION;
BEGIN
    IF (pamt<-999.99 or pamt>999.99) THEN
        RAISE PROD_OUTSIDE_RANGE;
    END IF;
    UPDATE PRODUCT
    SET SALES_YTD =SALES_YTD + pamt
    WHERE PRODID = pprodid;

    IF(SQL%NOTFOUND) THEN     
        RAISE PRODID_NOT_FOUND;     
    END IF;

EXCEPTION
    WHEN PRODID_NOT_FOUND THEN
       RAISE_APPLICATION_ERROR(-20103,'Product ID not found ');
    WHEN PROD_OUTSIDE_RANGE THEN
       RAISE_APPLICATION_ERROR(-20117,'Amount out of range ');
    WHEN OTHERS THEN
       RAISE_APPLICATION_ERROR(-20000 , SQLERRM);
END; 
/
create or replace PROCEDURE UPD_PROD_SALESYTD_VIASQLDEV (pprodid number,pamt number) as

BEGIN
    DBMS_OUTPUT.PUT_LINE('--------------------------------------------');
    DBMS_OUTPUT.PUT_LINE('Updating SalesYTD.' || pprodid ||' Amount: '|| pamt);
    UPD_PROD_SALESYTD_IN_DB(pprodid, pamt);     

    IF (SQL%FOUND) THEN         
        DBMS_OUTPUT.PUT_LINE('Update OK');         
        COMMIT;     
    END IF;
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE(SQLERRM);
END;
/

create or replace procedure UPD_CUST_STATUS_IN_DB (pcustid number,pstatus varchar2) AS
CUSTOMERID_NOT_FOUND EXCEPTION;
INVALID_STATUS EXCEPTION;
BEGIN
      IF(pstatus = 'OK' OR pstatus = 'SUSPEND') THEN       
        UPDATE CUSTOMER      
        SET STATUS=pstatus      
        WHERE CUSTID = pcustid; 

        IF(SQL%NOTFOUND) THEN             
            RAISE CUSTOMERID_NOT_FOUND;    
        END IF;

      ELSE        
        RAISE INVALID_STATUS;  
      END IF;
EXCEPTION
    WHEN CUSTOMERID_NOT_FOUND THEN
       RAISE_APPLICATION_ERROR(-20123,'Customer not found');
    WHEN INVALID_STATUS THEN
       RAISE_APPLICATION_ERROR(-20137,'Invalid Status value');
    WHEN OTHERS THEN
       RAISE_APPLICATION_ERROR(-20000 , SQLERRM);
END; 
/
create or replace PROCEDURE UPD_CUST_STATUS_VIASQLDEV (pcustid number,pstatus varchar2) as

BEGIN
    DBMS_OUTPUT.PUT_LINE('--------------------------------------------');
    DBMS_OUTPUT.PUT_LINE('Updating Status.' || pcustid ||' Status: '|| pstatus);
    UPD_CUST_STATUS_IN_DB(pcustid , pstatus);     

    IF (SQL%FOUND) THEN         
        DBMS_OUTPUT.PUT_LINE('Update OK');         
        COMMIT;     
    END IF;
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE(SQLERRM);
END;
/
create or replace PROCEDURE ADD_SIMPLE_SALE_TO_DB (pcustid NUMBER, pprodid NUMBER, pqty NUMBER) as
QTY_OUTSIDE_RANGE EXCEPTION;
INVALID_CUST_STATUS EXCEPTION;
NO_CUST_FOUND EXCEPTION;
NO_PROD_FOUND EXCEPTION;

vpprice NUMBER; 
vamt NUMBER; 
vstatus VARCHAR2(20);

BEGIN
    IF(pqty<0 or pqty >999) THEN     
        RAISE QTY_OUTSIDE_RANGE;     
    END IF; 

    SELECT SELLING_PRICE     INTO vpprice    
    FROM PRODUCT     
    WHERE PRODID = pprodid; 

    SELECT STATUS     INTO vstatus     
    FROM CUSTOMER     
    WHERE CUSTID = pcustid;  

    vamt:=(pqty * vpprice); 

    IF(vstatus != 'OK') THEN     
        RAISE INVALID_CUST_STATUS;    
    END IF;

    UPD_CUST_SALESYTD_IN_DB(pcustid,vamt);
    UPD_PROD_SALESYTD_IN_DB(pprodid,vamt);

EXCEPTION
    WHEN NO_DATA_FOUND THEN     
        IF vpprice IS NULL THEN        
            RAISE_APPLICATION_ERROR(-20175, 'Product ID not found');     
            END IF;     
        IF vstatus IS NULL THEN         
            RAISE_APPLICATION_ERROR(-20161, 'Customer ID not found'); 
        END IF;
    WHEN QTY_OUTSIDE_RANGE THEN
        RAISE_APPLICATION_ERROR(-20143,'Sale Quantity outside valid range');
    WHEN INVALID_CUST_STATUS THEN
       RAISE_APPLICATION_ERROR(-20157,'Customer status is not OK');
    WHEN OTHERS THEN
       RAISE_APPLICATION_ERROR(-20000 , SQLERRM);
END;
/
create or replace PROCEDURE ADD_SIMPLE_SALE_VIASQLDEV(pcustid NUMBER, pprodid NUMBER, pqty NUMBER) AS 
BEGIN     
    DBMS_OUTPUT.PUT_LINE('----------------------------------------------');    
    DBMS_OUTPUT.PUT_LINE('Adding Simple Sale. Cust Id:' ||pcustid|| ' Prod ID:' || pprodid||' Qty:' ||pqty);     
    ADD_SIMPLE_SALE_TO_DB(pcustid, pprodid, pqty);    

    IF(SQL%ROWCOUNT>0) THEN         
        DBMS_OUTPUT.PUT_LINE('Added Simple Sale OK');         
        COMMIT;     
    END IF;      

EXCEPTION     
    WHEN OTHERS THEN     
        DBMS_OUTPUT.PUT_LINE(SQLERRM); 
    END;
/
create or replace FUNCTION SUM_CUST_SALESYTD RETURN number AS

vSum number;

BEGIN
    SELECT SUM(SALES_YTD) 
    INTO vSum FROM CUSTOMER;  
    Return vSum;

EXCEPTION     
    WHEN OTHERS THEN     
        RAISE_APPLICATION_ERROR(-20000, SQLERRM);
END;
/
create or replace PROCEDURE SUM_CUST_SALES_VIASQLDEV  as
vsum number;
BEGIN
    DBMS_OUTPUT.PUT_LINE('--------------------------------------------');
    DBMS_OUTPUT.PUT_LINE('Summing Customer SalesYTD');
    vsum:= SUM_CUST_SALESYTD;   

    IF(SQL%FOUND) THEN     
        DBMS_OUTPUT.PUT_LINE('All Customer Total: '||vsum);     
    ELSE     
        DBMS_OUTPUT.PUT_LINE('All Cutomer Total: 0');     
    END IF;
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE(SQLERRM);
END;
/
create or replace FUNCTION SUM_PROD_SALESYTD_FROM_DB RETURN number AS

vSum number;

BEGIN
    SELECT SUM(SALES_YTD) 
    INTO vSum FROM PRODUCT;  
    Return vSum;

EXCEPTION     
    WHEN OTHERS THEN     
        RAISE_APPLICATION_ERROR(-20000, SQLERRM);
END;
/
create or replace PROCEDURE SUM_PROD_SALES_VIASQLDEV  as
vsum number;
BEGIN
    DBMS_OUTPUT.PUT_LINE('--------------------------------------------');
    DBMS_OUTPUT.PUT_LINE('Summing prodcut SalesYTD');
    vsum:= SUM_PROD_SALESYTD_FROM_DB;   

    IF(SQL%FOUND) THEN     
        DBMS_OUTPUT.PUT_LINE('All Product Total: '||vsum);     
    ELSE     
        DBMS_OUTPUT.PUT_LINE('All Product Total: 0');     
    END IF;
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE(SQLERRM);
END;
/
--======================================================================
--=============================Part2====================================
--======================================================================
create or replace FUNCTION GET_ALLCUST  
return SYS_REFCURSOR AS

custcursor SYS_REFCURSOR;
BEGIN
   OPEN custcursor FOR SELECT * FROM customer;
   RETURN custcursor;
EXCEPTION
    WHEN OTHERS THEN
       RAISE_APPLICATION_ERROR(-20000 , SQLERRM);
END;
/
create or replace PROCEDURE GET_ALLCUST_VIASQLDEV AS

custcursor SYS_REFCURSOR;
custdetail CUSTOMER%ROWTYPE;

BEGIN
    DBMS_OUTPUT.PUT_LINE('---------------------------------');    
    DBMS_OUTPUT.PUT_LINE('Listing all Customer Details');
    custcursor:= GET_ALLCUST;
    LOOP
        FETCH custcursor INTO custdetail;
        EXIT WHEN custcursor%NOTFOUND;
        DBMS_OUTPUT.PUT_LINE('CustID:'||custdetail.CUSTID||' Name:'||custdetail.CUSTNAME||' Status:'||custdetail.STATUS||' SalesYTD:'||custdetail.SALES_YTD);    
     END LOOP;
     
     IF (SQL%FOUND = FALSE) THEN         
        DBMS_OUTPUT.PUT_LINE('No rows found');     
     END IF;
EXCEPTION
    WHEN OTHERS THEN
       RAISE_APPLICATION_ERROR(-20000 , SQLERRM);
END;
/
create or replace FUNCTION GET_ALLPROD_FROM_DB 
return SYS_REFCURSOR AS

prodcursor SYS_REFCURSOR;
BEGIN
   OPEN prodcursor FOR SELECT * FROM product;
   RETURN prodcursor;
EXCEPTION
    WHEN OTHERS THEN
       RAISE_APPLICATION_ERROR(-20000 , SQLERRM);
END;
/
create or replace PROCEDURE GET_ALLPROD_VIASQLDEV AS

prodcursor SYS_REFCURSOR;
proddetail PRODUCT%ROWTYPE;

BEGIN
    DBMS_OUTPUT.PUT_LINE('---------------------------------');    
    DBMS_OUTPUT.PUT_LINE('Listing all Product Details');
    prodcursor:= GET_ALLPROD_FROM_DB;
    LOOP
        FETCH prodcursor INTO proddetail;
        EXIT WHEN prodcursor%NOTFOUND;
        DBMS_OUTPUT.PUT_LINE('ProdID: '||proddetail.PRODID||' Name:'||proddetail.PRODNAME||' Price:'||proddetail.SELLING_PRICE||' SalesYTD:'||proddetail.SALES_YTD);    
     END LOOP;
     
     IF (SQL%FOUND = FALSE) THEN         
        DBMS_OUTPUT.PUT_LINE('No rows found');     
     END IF;
EXCEPTION
    WHEN OTHERS THEN
       RAISE_APPLICATION_ERROR(-20000 , SQLERRM);
END;
/
--=================================================================
--=========================PART 3==================================
--=================================================================
create or replace FUNCTION strip_constraint(pErrmsg VARCHAR2 )  RETURN VARCHAR2 AS
brac_loca NUMBER; 
dot_loca NUMBER;
BEGIN  
    dot_loca := INSTR(pErrmsg , '.'); 
    brac_loca  := INSTR(pErrmsg , ')'); 

    IF (dot_loca = 0 OR brac_loca = 0 ) THEN      
        RETURN NULL ; 
    ELSE     
        RETURN UPPER(SUBSTR(pErrmsg,dot_loca+1,brac_loca-dot_loca-1));
    END IF;
END;
/
create or replace PROCEDURE ADD_LOCATION_TO_DB(ploccode varchar2, pminqty number, pmaxqty number) AS
DBMS_CONSTRAINT_NAME VARCHAR2(240);
INVALID_LOC_ID_LENGTH EXCEPTION;

BEGIN
IF Length(PLOCCODE) <> 5 THEN
RAISE INVALID_LOC_ID_LENGTH;
END IF; 
INSERT INTO LOCATION(LOCID, MINQTY, MAXQTY)
VALUES (PLOCCODE, PMINQTY, PMAXQTY);

EXCEPTION
WHEN DUP_VAL_ON_INDEX THEN
raise_application_error(-20183,	' Duplicate location ID');
WHEN INVALID_LOC_ID_LENGTH THEN   
raise_application_error(-20197, ' Location Code length invalid'); 
WHEN OTHERS THEN
    DBMS_CONSTRAINT_NAME:= STRIP_CONSTRAINT(SQLERRM);
    IF (DBMS_CONSTRAINT_NAME = 'CHECK_LOCID_LENGTH') THEN     
        raise_application_error(-20197, ' Location Code length invalid'); 
    ELSIF (DBMS_CONSTRAINT_NAME = 'CHECK_MINQTY_RANGE') THEN   
        raise_application_error(-20201, ' Minimum Qty out of range'); 
    ELSIF (DBMS_CONSTRAINT_NAME = 'CHECK_MAXQTY_RANGE') THEN
        raise_application_error(-20215, ' Maximum Qty out of range'); 
    ELSIF (DBMS_CONSTRAINT_NAME = 'CHECK_MAXQTY_GREATER_MIXQTY') THEN   
        raise_application_error(-20222, ' Minimum qty larger than maximum qty');   
    ELSE
        raise_application_error(-20000, sqlerrm);
    END IF;
END;
/
create or replace PROCEDURE ADD_LOCATION_VIASQLDEV (ploccode varchar2, pminqty number, pmaxqty number) AS

begin
dbms_output.put_line('--------------------------------------------');
dbms_output.put_line('Adding Location LocCode: ' || ploccode || ' MinQty: ' || pminqty || ' MaxQty: ' || pmaxqty);

add_location_to_db(ploccode, pminqty, pmaxqty);

IF(SQL%ROWCOUNT>0) THEN     
DBMS_OUTPUT.PUT_LINE('Location Added OK'); 
Commit;
END IF;
exception
when others then
dbms_output.put_line(SQLERRM);
END;
/
--=================================================================
--=========================PART 4==================================
--=================================================================
create or replace procedure ADD_COMPLEX_SALE_TO_DB(pcustid number, pprodid number, 
pqty number, pdate varchar2) as

INVALID_CUST_STATUS EXCEPTION; 
QTY_OUTSIDE_RANGE EXCEPTION; 
DATE_INVALID EXCEPTION; 

--CUST_ID_NOT_FOUND EXCEPTION; 
--PROD_ID_NOT_FOUND EXCEPTION;

vprice number;
vstatus varchar2(10);
vdate date;

PRAGMA EXCEPTION_INIT(DATE_INVALID, -01847); --Invalid day
PRAGMA EXCEPTION_INIT(DATE_INVALID, -01840); --Wrong format
PRAGMA EXCEPTION_INIT(DATE_INVALID, -01841); --Invalid Year
PRAGMA EXCEPTION_INIT(DATE_INVALID, -01843); --Invalid month
begin

SELECT STATUS INTO vstatus FROM CUSTOMER WHERE CUSTID = pcustid; 

IF (LENGTH(pdate) <> 8) THEN   
    RAISE DATE_INVALID;   
END IF;   

IF (pqty < 1 OR pqty > 999) THEN    
    RAISE QTY_OUTSIDE_RANGE; 
END IF;  
 
IF (vstatus != 'OK') THEN    
    RAISE INVALID_CUST_STATUS;   
END IF; 

vdate := to_date(pdate, 'YYYYMMDD');

SELECT SELLING_PRICE INTO vprice FROM PRODUCT WHERE PRODID= pprodid;

INSERT INTO SALE(SALEID,CUSTID,PRODID,QTY,PRICE,SALEDATE)
VALUES(SALE_SEQ.nextval,pcustid,pprodid,pqty,vprice,vdate); 


upd_cust_salesytd_in_db(pcustid, pqty*vprice);
upd_prod_salesytd_in_db(pprodid, pqty*vprice);

exception
    WHEN NO_DATA_FOUND THEN      
        IF vstatus IS NULL THEN        
            raise_application_error(-20265, 'Customer ID not found'); 
        END IF;      
        IF vprice IS NULL THEN          
            raise_application_error(-20272, 'Product ID not found');   
        END IF;   
    WHEN QTY_OUTSIDE_RANGE THEN   
        raise_application_error(-20233, 'Sale Quantity outside valid range');
    WHEN INVALID_CUST_STATUS THEN   
        raise_application_error(-20247, 'Customer status is not OK'); 
    WHEN DATE_INVALID THEN  
        raise_application_error(-20251, 'Date not valid');
    when others then
        raise_application_error(-20000, SQLERRM);
end;
/
create or replace procedure ADD_COMPLEX_SALE_VIASQLDEV(pcustid number, pprodid number, 
pqty number, pdate varchar2) as

vamt NUMBER; 
vprice NUMBER; 

begin
    BEGIN        
        SELECT SELLING_PRICE INTO vprice FROM PRODUCT WHERE PRODID=pprodid;     
        vamt:= pqty * vprice;      
    EXCEPTION    
        WHEN NO_DATA_FOUND THEN    
    NULL;   
    END; 

DBMS_OUTPUT.PUT_LINE('-------------------------------------------');
DBMS_OUTPUT.PUT_LINE('Adding Complex Sale. Cust ID:'||pcustid||' Prod ID:'||pprodid||' Date:'||pdate||' Amt:'||vamt);

add_complex_sale_to_db(pcustid, pprodid, pqty, pdate);

IF (SQL%ROWCOUNT>0) THEN     
    DBMS_OUTPUT.PUT_LINE('Added Complex Sale OK');  
    COMMIT;
END IF; 

exception
when others then 
DBMS_OUTPUT.PUT_LINE(SQLERRM);

end;
/
create or replace function GET_ALLSALES_FROM_DB return SYS_REFCURSOR as

rv_ref SYS_REFCURSOR;
begin

open rv_ref for select * from sale;
return rv_ref;

exception
when others then 
raise_application_error(-20000, sqlerrm);

end;
/
create or replace procedure GET_ALLSALES_VIASQLDEV as

rv_ref SYS_REFCURSOR;
sale_details sale%rowtype;
begin

dbms_output.put_line('-------------------------------------------');
dbms_output.put_line('Listing All Sales Details');

rv_ref := GET_ALLSALES_FROM_DB;



loop fetch rv_ref into sale_details;
exit when rv_ref%notfound;
dbms_output.put_line('SaleId:'  ||sale_details.saleid || ' CustId:' || sale_details.custid || ' ProdId:' || sale_details.prodid
                        || ' Date:' || sale_details.saledate || ' Amount:' || sale_details.qty*sale_details.price);

end loop;


IF (SQL%NOTFOUND) THEN        
DBMS_OUTPUT.PUT_LINE('No rows found');  
END IF;

close rv_ref;

exception
when others then 
DBMS_OUTPUT.PUT_LINE(SQLERRM);
end;
/

create or replace function COUNT_PRODUCT_SALES_FROM_DB (pdays number) return number as

vCount number;
begin

select count(saledate) into vCount
from sale
where (sysdate - saledate) < pdays;
return vCount;

exception
when others then 
raise_application_error(-20000, sqlerrm);

end;
/

create or replace procedure COUNT_PRODUCT_SALES_VIASQLDEV (pdays number) as

vCount number;
begin

dbms_output.put_line('-------------------------------------------');
dbms_output.put_line('Counting sales within ' || pdays || ' days');

vCount := count_product_sales_from_db(pdays);
IF (SQL%FOUND) THEN 
dbms_output.put_line('Total number of sales: ' || vCount);
END IF;
exception
when others then 
dbms_output.put_line(sqlerrm);
end;
/
create or replace function DELETE_SALE_FROM_DB return number as

vminsaleid SALE.SALEID%TYPE; 
vamt NUMBER; 
vqty SALE.QTY%TYPE; 
vprice SALE.PRICE%TYPE; 
vcustid SALE.CUSTID%TYPE; 
vprodid SALE.PRODID%TYPE; 

NO_SALE_ROWS_FOUND EXCEPTION;
begin

SELECT MIN(SALEID) INTO vminsaleid FROM SALE; 

IF vminsaleid IS NULL THEN      
    RAISE NO_SALE_ROWS_FOUND;  
ELSE
SELECT CUSTID,PRODID,QTY,PRICE INTO vcustid,vprodid,vqty,vprice 
FROM SALE WHERE SALEID=vminsaleid; 

DELETE FROM SALE WHERE SALEID=vminsaleid;

vamt := vqty*vprice;
UPD_CUST_SALESYTD_IN_DB(vcustid, (-vamt)); 
UPD_PROD_SALESYTD_IN_DB(vprodid, (-vamt));  

END IF;
return vminsaleid;

exception
when NO_SALE_ROWS_FOUND then
raise_application_error(-20283, 'No Sale Rows Found');
when others then 
raise_application_error(-20000, sqlerrm);
end;
/
create or replace PROCEDURE DELETE_SALE_VIASQLDEV AS 
vresult NUMBER; 
BEGIN    
    DBMS_OUTPUT.PUT_LINE('------------------------------------');   
    DBMS_OUTPUT.PUT_LINE('Deleting Sale with smallest SaleID value'); 
    vresult:=DELETE_SALE_FROM_DB;   
IF(SQL%FOUND) THEN       
    DBMS_OUTPUT.PUT_LINE('Deleted Sale OK. SaleID:'||vresult); 
    COMMIT;  
END IF;     
EXCEPTION   
    WHEN OTHERS THEN    
        DBMS_OUTPUT.PUT_LINE(SQLERRM); 
END;
/
create or replace PROCEDURE DELETE_ALL_SALES_FROM_DB as

rv_ref SYS_REFCURSOR;
cust_emp customer%rowtype;
prod_emp product%rowtype;

begin

delete from sale;

open rv_ref for select * from customer;

LOOP fetch rv_ref into cust_emp;
exit when rv_ref%notfound;
upd_cust_salesytd_in_db(cust_emp.custid, -cust_emp.sales_ytd);
end loop;

close rv_ref;

open rv_ref for select * from product;

LOOP fetch rv_ref into prod_emp;
exit when rv_ref%notfound;
upd_prod_salesytd_in_db(prod_emp.prodid, -prod_emp.sales_ytd);
end loop;

close rv_ref;

exception
when others then 
raise_application_error(-20000, sqlerrm);

end;
/
create or replace PROCEDURE DELETE_ALL_SALES_VIASQLDEV as

begin

DBMS_OUTPUT.PUT_LINE('---------------------------------------------');
DBMS_OUTPUT.PUT_LINE('Deleting all Sales data in Sale, Customer, and Product tables');

delete_all_sales_from_db;
IF (SQL%FOUND) THEN        
DBMS_OUTPUT.PUT_LINE('Deletion OK');   
COMMIT;
END IF;

EXCEPTION
WHEN OTHERS THEN
DBMS_OUTPUT.PUT_LINE(SQLERRM);
end;
/
--=================================================================
--=========================PART 5==================================
--=================================================================
create or replace procedure DELETE_CUSTOMER(pcustid number) as
CUST_ID_NOT_FOUND exception;
CUST_HAS_CHILD_ROW exception;

PRAGMA EXCEPTION_INIT(CUST_HAS_CHILD_ROW, -2292);

begin

DELETE FROM CUSTOMER WHERE CUSTID=pCustid;  

IF SQL%NOTFOUND THEN       
    RAISE CUST_ID_NOT_FOUND;   
END IF;


exception
when CUST_ID_NOT_FOUND then
raise_application_error(-20293, 'Customer ID not found');
when CUST_HAS_CHILD_ROW then
raise_application_error(-20307, 'Customer cannot be deleted as sales exist');
when others then 
raise_application_error(-20000, sqlerrm);

end;
/
CREATE OR REPLACE PROCEDURE DELETE_CUSTOMER_VIASQLDEV(pCustid NUMBER) AS 
BEGIN    
DBMS_OUTPUT.PUT_LINE('----------------------------------');  
DBMS_OUTPUT.PUT_LINE('Deleting Customer. CustID:'||pCustid);  
DELETE_CUSTOMER(pCustid);    
IF (SQL%FOUND) THEN       
    DBMS_OUTPUT.PUT_LINE('Deleted Customer OK');   
    COMMIT;    
END IF;      
EXCEPTION    
    WHEN OTHERS THEN   
    DBMS_OUTPUT.PUT_LINE(SQLERRM);
END;
/
create or replace procedure DELETE_PROD_FROM_DB(pprodid number) as
PROD_HAS_CHILD_ROW EXCEPTION;
PROD_ID_NOT_FOUND EXCEPTION;

PRAGMA EXCEPTION_INIT(PROD_HAS_CHILD_ROW, -2292);
begin

DELETE FROM PRODUCT WHERE PRODID=pProdid;    
IF SQL%NOTFOUND THEN    
    RAISE PROD_ID_NOT_FOUND;  
END IF;

exception
when PROD_ID_NOT_FOUND then
    raise_application_error(-20313, 'Product ID not found');
when PROD_HAS_CHILD_ROW then
    raise_application_error(-20327, 'Product cannot be deleted as sales exist');
when others then 
    raise_application_error(-20000, sqlerrm);
end;
/
CREATE OR REPLACE PROCEDURE DELETE_PROD_VIASQLDEV(pProdid NUMBER) AS
BEGIN    
DBMS_OUTPUT.PUT_LINE('----------------------------------');   
DBMS_OUTPUT.PUT_LINE('Deleting Product. Product id:'||pProdid);   
DELETE_PROD_FROM_DB(pProdid);    
IF (SQL%FOUND) THEN      
    DBMS_OUTPUT.PUT_LINE('Deleted Product OK');       
    COMMIT;   
END IF;       
EXCEPTION   
    WHEN OTHERS THEN  
    DBMS_OUTPUT.PUT_LINE(SQLERRM);
END;
/