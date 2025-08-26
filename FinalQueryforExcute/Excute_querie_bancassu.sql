---/// motor tables----------------------------------->>>>
drop SEQUENCE QUOTATION.banc_id_seq 
CREATE SEQUENCE QUOTATION.banc_id_seq
    INCREMENT BY 1
    START WITH 1
    MINVALUE 1
    MAXVALUE 999999999999999999999999999
    CACHE 20;
    
CREATE TABLE QUOTATION.BANK_SALES_OFFICER
  (EPF VARCHAR2(10 BYTE) NOT NULL ENABLE, 
   BANK_CODE VARCHAR2(12 BYTE) NOT NULL ENABLE, 
   BANK_NAME VARCHAR2(60 BYTE), 
   OFFICER_NAME VARCHAR2(60 BYTE),
   DESIGNATION VARCHAR2(40 BYTE),
   CONTACT_NO VARCHAR2(12 BYTE),
   EMAIL VARCHAR2(60 BYTE),
   CREATED_BY VARCHAR2(10 BYTE), 
   CREATED_ON DATE,
   FLAG VARCHAR2(1 BYTE), 
   
   CONSTRAINT PK_SO PRIMARY KEY (EPF, BANK_CODE)
   );  

 INSERT ALL
INTO QUOTATION.BANK_SALES_OFFICER VALUES ('8264','7311','PABC','Prasath Madushanka','Sales Officer',
'0777794707','prasathh@srilankainsurance.com', '9641',sysdate,'A')
INTO QUOTATION.BANK_SALES_OFFICER VALUES ('9458','7311','PABC','Nishan Edirisinghe','Underwriter',
'0762037876','nishane@srilankainsurance.com', '9641',sysdate,'A')

INTO QUOTATION.BANK_SALES_OFFICER VALUES ('7696','7010','BOC','Ruween H.N.','Sales Officer',
'0773896479','ruweenn@srilankainsurance.com', '9641',sysdate,'A')
INTO QUOTATION.BANK_SALES_OFFICER VALUES ('8857','7010','BOC','Pasindu Tammitge','Sales Officer',
'0772814921','pasindut@srilankainsurance.com', '9641',sysdate,'A')
INTO QUOTATION.BANK_SALES_OFFICER VALUES ('8569','7010','BOC','Chathika Angunawala','Underwriter',
'0771938091','chathikaa@srilankainsurance.com', '9641',sysdate,'A')

INTO QUOTATION.BANK_SALES_OFFICER VALUES ('8857','7755','RDB','Pasindu Tammitge','Sales Officer',
'0772814921','pasindut@srilankainsurance.com', '9641',sysdate,'A')
INTO QUOTATION.BANK_SALES_OFFICER VALUES ('8569','7755','RDB','Chathika Angunawala','Underwriter',
'0771938091','chathikaa@srilankainsurance.com', '9641',sysdate,'A')

INTO QUOTATION.BANK_SALES_OFFICER VALUES ('8857','7728','SANASA','Pasindu Tammitge','Sales Officer',
'0772814921','pasindut@srilankainsurance.com', '9641',sysdate,'A')
INTO QUOTATION.BANK_SALES_OFFICER VALUES ('8569','7728','SANASA','Chathika Angunawala','Underwriter',
'0771938091','chathikaa@srilankainsurance.com', '9641',sysdate,'A')

SELECT * FROM dual;  
   
  
CREATE TABLE QUOTATION.BANK_REQ_ENTRY_DETAILS
(
  REQ_ID        VARCHAR2(12) NOT NULL,
  BANK_CODE     VARCHAR2(12),
  V_TYPE        VARCHAR2(60),
  YOM           NUMBER(4),
  SUM_INSU      NUMBER(16,2),
  V_MAKE        NUMBER(4),
  V_MODEL       NUMBER(4),
  PURPOSE       VARCHAR2(12),
  V_REG_NO      VARCHAR2(20),
  CUS_NAME      VARCHAR2(20),
  CUS_PHONE     VARCHAR2(16),
  V_FUEL        VARCHAR2(20),
  ENTERED_BY    VARCHAR2(10),
  ENTERED_ON    DATE,
  EMAIL         VARCHAR2(60),
  FLAG          VARCHAR2(2),
  QUO_NO        VARCHAR2(20),
  BRANCH_CODE   VARCHAR2(12),
  EMAIL_SEND_BY VARCHAR2(12),
  EMAIL_SEND_ON DATE,
  BANC_EMAIL    VARCHAR2(60),
  OTHER_MODEL   VARCHAR2(60),
  PRIMARY KEY(REQ_ID)
)

  insert into SLIC_CNOTE.VH_MAKES values ('Dump',0); 
  insert into SLIC_CNOTE.VH_MODEL values(0,0,'Dump1');
 
---/// End----------------------------------->>>>    




---///------------------Fire Tables------------>>>>

drop SEQUENCE QUOTATION.FIRE_DH_ID_SEQ 
CREATE SEQUENCE QUOTATION.FIRE_DH_ID_SEQ 
    INCREMENT BY 1
    START WITH 1
    MINVALUE 1
    MAXVALUE 999999999999999999999999999
    CACHE 20;
---------------------------------------------------------------------------------------
DROP TABLE QUOTATION.BANCASU_RATE
CREATE TABLE QUOTATION.BANCASU_RATE
 (DEPARTMENT VARCHAR2(3 BYTE) NOT NULL ENABLE, 
  D_TYPE VARCHAR2(4 BYTE), 
  BANK VARCHAR2(6 BYTE)  NOT NULL ENABLE, 
  BANK_CODE VARCHAR2(6 BYTE)  NOT NULL ENABLE,
  BASIC NUMBER(8,6), 
  RCC NUMBER(8,6),
  TR NUMBER(8,6),
  ADMIN_FEE NUMBER(8,6),
  POLICY_FEE NUMBER(8,4),
  EFFECTIVE_DATE DATE,
  CREATED_ON DATE,
  CREATED_BY VARCHAR2(12)
   ); 

INSERT INTO QUOTATION.BANCASU_RATE VALUES ('F','PD','BOC','7010',0.04,0.03,0.0125,0.35,500,SYSDATE,SYSDATE,USER);

--SELECT * FROM QUOTATION.BANCASU_RATE
---------------------------------------------------------------------------------------------------------------

---------------------------------------------------------------------------------------
DROP TABLE QUOTATION.FIRE_DH_SCHEDULE_CALC
CREATE TABLE QUOTATION.FIRE_DH_SCHEDULE_CALC
 (SC_REF_NO VARCHAR2(12 BYTE) NOT NULL ENABLE,
  SC_POLICY_NO VARCHAR2(16 BYTE), 
  SC_SUM_INSU NUMBER(16,2),
  SC_NET_PRE NUMBER(16,2),
  SC_RCC NUMBER(10,2),
  SC_TR NUMBER(10,2),
  SC_ADMIN_FEE NUMBER(10,2),
  SC_POLICY_FEE NUMBER(10,2),
  SC_NBT NUMBER(10,2),
  SC_VAT NUMBER(10,2),
  SC_TOTAL_PAY NUMBER(16,2),
  CREATED_ON DATE,
  CREATED_BY VARCHAR2(12),
  UPDATED_ON DATE,
  UPDATED_BY VARCHAR2(12),
  FLAG VARCHAR2(1 BYTE),
  PRIMARY KEY (SC_REF_NO)
   ); 

--SELECT * FROM QUOTATION.FIRE_DH_SCHEDULE_CALC


---------------------------------------------------------------------------------------
DROP TABLE QUOTATION.FIRE_DH_SCHEDULE_CALC_HISTORY
CREATE TABLE QUOTATION.FIRE_DH_SCHEDULE_CALC_HISTORY
 (SC_REF_NO VARCHAR2(12 BYTE) NOT NULL ENABLE,
  SC_POLICY_NO VARCHAR2(16 BYTE), 
  SC_SUM_INSU NUMBER(16,2),
  SC_NET_PRE NUMBER(16,2),
  SC_RCC NUMBER(10,2),
  SC_TR NUMBER(10,2),
  SC_ADMIN_FEE NUMBER(10,2),
  SC_POLICY_FEE NUMBER(10,2),
  SC_NBT NUMBER(10,2),
  SC_VAT NUMBER(10,2),
  SC_TOTAL_PAY NUMBER(16,2),
  CREATED_ON DATE,
  CREATED_BY VARCHAR2(12),
  FLAG VARCHAR2(1 BYTE)
   ); 

--SELECT * FROM QUOTATION.FIRE_DH_SCHEDULE_CALC_HISTORY
---------------------------------------------------------------------------------------

---------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------
DROP TABLE QUOTATION.FIRE_DH_SCOPE_OF_COVERS
CREATE TABLE QUOTATION.FIRE_DH_SCOPE_OF_COVERS
 (SEQ_ID NUMBER(2),
  DH_SCOPE_WORD VARCHAR2(400 BYTE),
  ACTIVE VARCHAR2(1 BYTE),
  PRIMARY KEY (SEQ_ID)
  );  
INSERT ALL
INTO QUOTATION.FIRE_DH_SCOPE_OF_COVERS VALUES (1,'Fire & Lightning','Y')
INTO QUOTATION.FIRE_DH_SCOPE_OF_COVERS VALUES (2,'Strike, Riot & Civil Commotion(Up to the Sum Insured)','Y')
INTO QUOTATION.FIRE_DH_SCOPE_OF_COVERS VALUES (3,'Terrorism (Up to the Sum Insured)','Y')
INTO QUOTATION.FIRE_DH_SCOPE_OF_COVERS VALUES (4,'Malicious Damage','Y')
INTO QUOTATION.FIRE_DH_SCOPE_OF_COVERS VALUES (5,'Explosion','Y')
INTO QUOTATION.FIRE_DH_SCOPE_OF_COVERS VALUES (6,'Cyclone, Storm, Tempest','Y')
INTO QUOTATION.FIRE_DH_SCOPE_OF_COVERS VALUES (7,'Flood','Y')
INTO QUOTATION.FIRE_DH_SCOPE_OF_COVERS VALUES (8,'Earthquake with Fire & Shock','Y')
INTO QUOTATION.FIRE_DH_SCOPE_OF_COVERS VALUES (9,'Natural Disaster (Including Tsunami, Tidal Waves, Volcanic Eruption, Tornadoes due to an atmospheric disturbance, Hurricane, Typhoon, Thunderstorm, Hailstorm, Windstorm Rainstorm due to an atmospheric disturbance so designated by the Meteorological Department subject to exceptions/exclusions mentioned under the Cyclone, Storm and Tempest cover granted under the standard fire policy).','Y')
INTO QUOTATION.FIRE_DH_SCOPE_OF_COVERS VALUES (10,'Aircraft Damage','Y')
INTO QUOTATION.FIRE_DH_SCOPE_OF_COVERS VALUES (11,'Impact Damage','Y')
INTO QUOTATION.FIRE_DH_SCOPE_OF_COVERS VALUES (12,'Bursting or Overflowing of Water Tanks, Apparatus or pipes','Y')
INTO QUOTATION.FIRE_DH_SCOPE_OF_COVERS VALUES (13,'Electrical Inclusion Clause','Y')
SELECT * FROM dual;
--SELECT * FROM QUOTATION.FIRE_DH_SCOPE_OF_COVERS
---------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------
DROP TABLE QUOTATION.FIRE_DH_DEDUCTIBLES
 CREATE TABLE QUOTATION.FIRE_DH_DEDUCTIBLES
  (SEQ_ID NUMBER(2),
  DH_MIN_VAL NUMBER(10),
  DH_MAX_VAL NUMBER(10),
  DH_OPTION1 VARCHAR2(200 BYTE),
  DH_OPTION2 VARCHAR2(200 BYTE),
  DH_OPTION3 VARCHAR2(200 BYTE),
  DH_OPTION4 VARCHAR2(200 BYTE),
  ACTIVE VARCHAR2(1 BYTE),
  PRIMARY KEY (SEQ_ID)
  );  
  
INSERT ALL
INTO QUOTATION.FIRE_DH_DEDUCTIBLES VALUES (1,1000000,2500000,
'Srike, Riot & Civil Commotion - 10 % but a maximum of Rs.100,000.00 on Each and Every Loss.',
'Terrorism  - 10 % but a maximum of Rs.100,000.00 on Each and Every Loss.',
'All Natural Perils including Cyclone/Strom/Tempest/Flood & Earthquake - 10% with a minimum of Rs. 2,500/-  on each & every loss.',
'All other Perils - 10% with a minimum of Rs. 1,000/-  on each & every loss.',
'Y')
INTO QUOTATION.FIRE_DH_DEDUCTIBLES VALUES (2,2500000,5000000,
'Srike, Riot & Civil Commotion - 10 % but a maximum of Rs.100,000.00 on Each and Every Loss.',
'Terrorism  - 10 % but a maximum of Rs.100,000.00 on Each and Every Loss.',
'All Natural Perils including Cyclone/Strom/Tempest/Flood & Earthquake - 10% with a minimum of Rs. 5,000/-  on each & every loss.',
'All other Perils - 10% with a minimum of Rs. 1,500/-  on each & every loss.',
'Y')

INTO QUOTATION.FIRE_DH_DEDUCTIBLES VALUES (3,5000000,7500000,
'Srike, Riot & Civil Commotion - 10 % but a maximum of Rs.100,000.00 on Each and Every Loss.',
'Terrorism  - 10 % but a maximum of Rs.100,000.00 on Each and Every Loss.',
'All Natural Perils including Cyclone/Strom/Tempest/Flood & Earthquake - 10% with a minimum of Rs. 5,000/-  on each & every loss.',
'All other Perils - 10% with a minimum of Rs. 2,000/-  on each & every loss.',
'Y')

INTO QUOTATION.FIRE_DH_DEDUCTIBLES VALUES (4,7500000,10000000,
'Srike, Riot & Civil Commotion - 10 % but a maximum of Rs.100,000.00 on Each and Every Loss.',
'Terrorism  - 10 % but a maximum of Rs.100,000.00 on Each and Every Loss.',
'All Natural Perils including Cyclone/Strom/Tempest/Flood & Earthquake - 10% with a minimum ofRs. 7,500/-  on each & every loss.',
'All other Perils - 10% with a minimum of Rs. 2,500/-  on each & every loss.',
'Y')

INTO QUOTATION.FIRE_DH_DEDUCTIBLES VALUES (5,10000000,15000000,
'Srike, Riot & Civil Commotion - 10 % but a maximum of Rs.100,000.00 on Each and Every Loss.',
'Terrorism  - 10 % but a maximum of Rs.100,000.00 on Each and Every Loss.',
'All Natural Perils including Cyclone/Strom/Tempest/Flood & Earthquake - 10% with a minimum of Rs. 10,000/-  on each & every loss.',
'All other Perils - 10% with a minimum of Rs. 3,000/-  on each & every loss.',
'Y')

INTO QUOTATION.FIRE_DH_DEDUCTIBLES VALUES (6,15000000,20000000,
'Srike, Riot & Civil Commotion - 10 % but a maximum of Rs.100,000.00 on Each and Every Loss.',
'Terrorism  - 10 % but a maximum of Rs.100,000.00 on Each and Every Loss.',
'All Natural Perils including Cyclone/Strom/Tempest/Flood & Earthquake - 10% with a minimum of Rs. 12,500/-  on each & every loss',
'All other Perils - 10% with a minimum of Rs. 4,000/-  on each & every loss.',
'Y')

INTO QUOTATION.FIRE_DH_DEDUCTIBLES VALUES (7,20000000,25000000,
'Srike, Riot & Civil Commotion - 10 % but a maximum of Rs.100,000.00 on Each and Every Loss.',
'Terrorism  - 10 % but a maximum of Rs.100,000.00 on Each and Every Loss.',
'All Natural Perils including Cyclone/Strom/Tempest/Flood & Earthquake - 10% with a minimum of Rs. 15,000/-  on each & every loss.',
'All other Perils - 10% with a minimum of Rs. 5,000/-  on each & every loss.',
'Y')

INTO QUOTATION.FIRE_DH_DEDUCTIBLES VALUES (8,25000000,30000000,
'Srike, Riot & Civil Commotion - 10 % but a maximum of Rs.100,000.00 on Each and Every Loss.',
'Terrorism  - 10 % but a maximum of Rs.100,000.00 on Each and Every Loss.',
'All Natural Perils including Cyclone/Strom/Tempest/Flood & Earthquake - 10% with a minimum of Rs. 20,000/-  on each & every loss.',
'All other Perils - 10% with a minimum of Rs. 7,500/-  on each & every loss.',
'Y')
SELECT * FROM dual;
  
--SELECT * FROM QUOTATION.FIRE_DH_DEDUCTIBLES
---------------------------------------------------------------------------------------------
DROP TABLE QUOTATION.FIRE_DH_PROPOSAL_ENTRY

CREATE TABLE QUOTATION.FIRE_DH_PROPOSAL_ENTRY
  (DH_REFNO VARCHAR2(12 BYTE) NOT NULL ENABLE,  --REF_NO
   DH_BCODE VARCHAR2(60 BYTE),                   --BANK CODE NAME
   DH_BCODE_ID VARCHAR2(10 BYTE),                   --BANK CODE 
   DH_BBRCODE VARCHAR2(60 BYTE),                 --BRANCH CODE NAME
   DH_BBRCODE_ID VARCHAR2(10 BYTE),                 --BRANCH CODE
   DH_NAME VARCHAR2(100 BYTE),                  --PROPSER NAME
   DH_AGECODE VARCHAR2(20 BYTE),                 --AGENT CODE
   DH_AGENAME VARCHAR2(20 BYTE),                 --AGENT NAME
   DH_NIC VARCHAR2(12 BYTE),                    --NIC
   DH_BR VARCHAR2(12 BYTE),                     --BR CODE 
   DH_PADD1 VARCHAR2(20 BYTE),                  --POSTAL ADD1
   DH_PADD2 VARCHAR2(60 BYTE),                  --POSTAL ADD2
   DH_PADD3 VARCHAR2(60 BYTE),                  --POSTAL ADD3
   DH_PADD4 VARCHAR2(60 BYTE),                  --POSTAL ADD4
   DH_PHONE VARCHAR2(16 BYTE),                  --PHONE
   DH_EMAIL VARCHAR2(40 BYTE),                  --EMAIL
   DH_IADD1 VARCHAR2(20 BYTE),                  --INSURED ADD1
   DH_IADD2 VARCHAR2(60 BYTE),                  --INSURED ADD2
   DH_IADD3 VARCHAR2(60 BYTE),                  --INSURED ADD3
   DH_IADD4 VARCHAR2(60 BYTE),                  --INSURED ADD4
   DH_PFROM DATE,                               --FROM DATE
   DH_PTO DATE,                                 --TO DATE
   DH_UCONSTR VARCHAR2(1 BYTE),                 --UNDER CONSTRUCTION YES OR NO
   DH_OCCU_CAR VARCHAR2(1 BYTE),                --OCCUPATION CARRIED YES OR NO
   DH_OCC_YES_REAS VARCHAR2(60 BYTE),           --IF OCCU YES REASON
   DH_HAZ_OCCU  VARCHAR2(1 BYTE),               --HAZORDUS YES OR NO
   DH_HAZ_YES_REA VARCHAR2(60 BYTE),            --HAZORDUS YES REASON
   DH_VALU_BUILD NUMBER(16,2),                  -- VALUE OF BUILDING
   DH_VALU_WALL NUMBER(16,2),                   -- VALUE OF WALL
   DH_VALU_TOTAL NUMBER(16,2),                  --TOTAL SUM ASSURED
   DH_NO_OF_FLOORS NUMBER(3),                   --NUMBER OF FLOORS
   DH_AFF_FLOOD VARCHAR2(1 BYTE),               --AFFECTED FLOOD DURING 5 YEARS YES OR NO
   DH_AFF_YES_REAS VARCHAR2(60 BYTE),           -- AFF. FLOOD YES REAASON
   DH_WBRICK VARCHAR2(1 BYTE),                  --WALL BRICK YES OR NO
   DH_WCEMENT VARCHAR2(1 BYTE),                 --WALL CEMENT YES OR NO
   DH_DWOODEN VARCHAR2(1 BYTE),                 --DOOR WINDOWS WOODEN YES OR NO
   DH_DMETAL VARCHAR2(1 BYTE),                  --DOOR WINDOWS METAL YES OR NO
   DH_FTILE VARCHAR2(1 BYTE),                   --FLOOR TILE YES OR NO
   DH_FCEMENT VARCHAR2(1 BYTE),                 --FLOOR CEMENT YES OR NO
   DH_RTILE VARCHAR2(1 BYTE),                   --ROOF TILE YES OR NO
   DH_RASBES VARCHAR2(1 BYTE),                  --ROOF ASBASTOSE YES OR NO
   DH_RGI VARCHAR2(1 BYTE),                     --ROOF GI YES OR NO
   DH_RCONCREAT VARCHAR2(1 BYTE),               --ROOF CONCREAT YES OR NO
   DH_COV_FIRE VARCHAR2(1 BYTE),                --COVER FIRE YES OR NOT 
   DH_COV_LIGHT VARCHAR2(1 BYTE),               --COVER LIGHT YES OR NOT 
   DH_COV_FLOOD VARCHAR2(1 BYTE),               --COVER FLOOD YES OR NOT 
   DH_CFWATERAVL VARCHAR2(1 BYTE),              --COVER FLOOD WATER COURSE AVALIBLE YES OR NO
   DH_CFYESR1 VARCHAR2(60 BYTE),                --COVER FLOOD WATER COURSE AVALIBLE REASON1
   DH_CFYESR2 VARCHAR2(60 BYTE),                --COVER FLOOD WATER COURSE AVALIBLE REASON2
   DH_CFYESR3 VARCHAR2(60 BYTE),                --COVER FLOOD WATER COURSE AVALIBLE REASON3
   DH_CFYESR4 VARCHAR2(60 BYTE),                --COVER FLOOD WATER COURSE AVALIBLE REASON4
   DH_ENTERED_BY VARCHAR2(12 BYTE),             --ENTERD BY
   DH_ENTERED_ON DATE,                          --ENTERDE DATE
   DH_HOLD VARCHAR2(1 BYTE),                    --HOLD FLAG FOR SLIC
   DH_OVER_VAL VARCHAR2(1 BYTE),                --HOLD FLAG FOR OVER 30M SLIC
   DH_FINAL_FLAG VARCHAR2(1 BYTE),               --HOLD FLAG FOR IF ONE OF CONDITION IS TRUE 
   DH_ISREQ      VARCHAR2(1),                    --VIEW PREMIUM ONLY
   DH_CONDITIONS VARCHAR2(500),                  -- CONDTIONS ADD BY UNDERWRITERS
   DH_ISREJECT VARCHAR2(1),                      --REJECT BY SLIC R OR N
   DH_ISCODI VARCHAR2(1),                        --CODITION ADDED BY SLIC YES OR NO
   DH_LOADING VARCHAR2(1),                        --LOADING ADD
   DH_LOADING_VAL VARCHAR2(6),                     --LOADING VAL
   CONSTRAINT PK_DH_REFNO PRIMARY KEY (DH_REFNO)
   ); 
   
   --SELECT * FROM QUOTATION.FIRE_DH_PROPOSAL_ENTRY;
   
   ---//Agent Code------------------------------------------------------
    CREATE TABLE QUOTATION.FIRE_AGENT_INFO
  (
  AGENCY_CODE NUMBER(10),
  AGE_NAME VARCHAR2(16),
  BANCASS_GI NUMBER(4),
  BANK_CODE VARCHAR2(6 BYTE),
  BANK_ACC VARCHAR2(20),
  CREATED_BY VARCHAR2(20 BYTE),
  CREATED_ON DATE,
  ACTIVE VARCHAR2(1 BYTE),
  PRIMARY KEY (AGENCY_CODE)
  ); 
  
  insert into QUOTATION.FIRE_AGENT_INFO values('780585','BOC','192','7010','82022680',user,sysdate,'Y');
  --SELECT * FROM  QUOTATION.FIRE_AGENT_INFO
  --------------------------------------------------------------------------
   
  ---//POLICY NUMBER LAST 4 DIGITS------------------------------------------------------
  CREATE TABLE QUOTATION.FIRE_POLICY_SEQ
  (P_TYPE VARCHAR2(6),
  P_YEAR VARCHAR2(6 BYTE),
  P_MONTH VARCHAR2(6 BYTE),
  P_SEQ VARCHAR2(6 BYTE),
  CREATED_ON DATE,
  ACTIVE VARCHAR2(1 BYTE)
  ); 
  
  --SELECT * FROM  QUOTATION.FIRE_POLICY_SEQ
  --------------------------------------------------------------------------
  
  
  
  
  CREATE TABLE QUOTATION.FIRE_DH_CONTACT_EMAILS
   (USER_CODE VARCHAR2(10 BYTE) NOT NULL ENABLE, 
   BANK_CODE VARCHAR2(12 BYTE) NOT NULL ENABLE, 
   BANK_NAME VARCHAR2(60 BYTE), 
   OFFICER_NAME VARCHAR2(60 BYTE),  
   EMAIL VARCHAR2(60 BYTE),
   EMAIL_CC VARCHAR2(2 BYTE),
   CREATED_BY VARCHAR2(10 BYTE), 
   CREATED_ON DATE,
   FLAG VARCHAR2(1 BYTE), 
   
   CONSTRAINT PK_CE PRIMARY KEY (USER_CODE, BANK_CODE)
   ); 
   

  
   INSERT ALL
INTO QUOTATION.FIRE_DH_CONTACT_EMAILS VALUES ('0001','7010','BOC','COMMON EMAIL FOR BOC','bancagiboc@srilankainsurance.com', 'N','9641',sysdate,'A')
INTO QUOTATION.FIRE_DH_CONTACT_EMAILS VALUES ('0001','7135','PEOPLES BANK ','COMMON EMAIL FOR PEOPLES','bancagipb@srilankainsurance.com','N', '9641',sysdate,'A')

INTO QUOTATION.FIRE_DH_CONTACT_EMAILS VALUES ('0002','7010','BOC','KANCHANA JAYAWARDHANA','kanchanaj@srilankainsurance.com', 'Y','9641',sysdate,'A')
INTO QUOTATION.FIRE_DH_CONTACT_EMAILS VALUES ('0002','7135','PEOPLES BANK ','KANCHANA JAYAWARDHANA','kanchanaj@srilankainsurance.com', 'Y','9641',sysdate,'A')

INTO QUOTATION.FIRE_DH_CONTACT_EMAILS VALUES ('0003','7010','BOC','NUWAN PRIYADHRSHANA','nuwanp@srilankainsurance.com', 'Y','9641',sysdate,'A')
INTO QUOTATION.FIRE_DH_CONTACT_EMAILS VALUES ('0003','7135','PEOPLES BANK ','NUWAN PRIYADHRSHANA','nuwanp@srilankainsurance.com', 'Y','9641',sysdate,'A')

INTO QUOTATION.FIRE_DH_CONTACT_EMAILS VALUES ('0005','7010','BOC','MADUSHA BANDARA','madushab@srilankainsurance.com', 'N','9641',sysdate,'A')
INTO QUOTATION.FIRE_DH_CONTACT_EMAILS VALUES ('0005','7135','PEOPLES BANK ','MADUSHA BANDARA','madushab@srilankainsurance.com', 'N','9641',sysdate,'A')
SELECT * FROM dual;



-----------------------------------------
insert into BGENERAL.NODESTABLE values
(587,'Bancassurance Online','No',92,'/SessionTrans/SessionManager.asp?fileindex=177')
---/// End---------------------------24092020-------->>>>  
