  CREATE OR REPLACE PROCEDURE QUOTATION.PROC_BANCASSU_BORDEREAUX(
 polNumber in varchar2, netPremium in number , startDate in date , srccVal in number , tcVal in number, noOfYears in number, created_by in varchar2, responseVal out number
)
  
  AS
  
  firstNetVal number := 0;
  firstSrcc number := 0;
  firstTc number := 0;
  --responseVal :=0;
   
   BEGIN
 if(polNumber is not null) then
     
     firstNetVal := round((netPremium/noOfYears),2);
     firstSrcc := round((srccVal/noOfYears),2);
     firstTc := round((tcVal/noOfYears),2);
     ---insert to table----------
     for lcounter in 0..(noOfYears-1)
       loop
     insert into QUOTATION.FIRE_DH_BANCASSU_BORDEREAUX(
     SEQ_ID,
     SC_POLICY_NO,
     SC_COMMENCEMENT_DATE,
     SC_NET_PRE,
     SC_RCC,
     SC_TR,
     CREATED_ON,
     CREATED_BY)
     VALUES
     (
     QUOTATION.BANCASSU_BORDEREAUX_SEQ.NEXTVAL,
     polNumber,
     trunc(add_months(startDate, lcounter * 12)),
     --trunc(startDate +lcounter),
     firstNetVal,
     firstSrcc,
     firstTc,
     sysdate,
     created_by);

     end loop;
     Commit;
     ----------------------------
     responseVal :=1;
     else
     responseVal :=0;
     end if;
      --dbms_output.put_line(responseVal);
        
 EXCEPTION
    WHEN OTHERS THEN
      Rollback;
      raise_application_error(-20001,
                              'An error was encountered - ' || SQLCODE ||
                              ' -ERROR- ' || SQLERRM);
                              
 END PROC_BANCASSU_BORDEREAUX;
