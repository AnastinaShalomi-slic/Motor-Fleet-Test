ALTER TABLE QUOTATION.BANK_REQ_ENTRY_DETAILS    
ADD (SLIC_REMARK VARCHAR2(500)) 
 
ALTER TABLE QUOTATION.FIRE_DH_PROPOSAL_ENTRY    
ADD (TERM VARCHAR2(2), Period NUMBER(6,2),Fire_cover VARCHAR2(2),
Other_cover VARCHAR2(2),SRCC_cover VARCHAR2(2),TC_cover VARCHAR2(2),Flood_cover VARCHAR2(2),BANK_UPDATED_BY VARCHAR2(12),
BANK_UPDATED_ON DATE,PROP_TYPE VARCHAR2(2),DH_SOLAR_SUM NUMBER(16,2),SOLAR_REPAIRE VARCHAR2(2),SOLAR_PARTS VARCHAR2(2),
SOLAR_ORIGIN VARCHAR2(60),SOLAR_SERIAL VARCHAR2(100),Solar_Period NUMBER(6,2));
 

ALTER TABLE QUOTATION.BANCASU_RATE add  Renewal_FEE NUMBER(10,4);
ALTER TABLE QUOTATION.BANCASU_RATE add (BASIC_2 NUMBER(8,6),TERM VARCHAR2(2));

update QUOTATION.BANCASU_RATE set RENEWAL_FEE =100,BASIC_2=0.03,TERM='A'
where BASIC =0.04
and bank_code='7010'


insert into QUOTATION.BANCASU_RATE (department,d_type,BANK,BANK_CODE,BASIC,RCC,TR,ADMIN_FEE,POLICY_FEE,EFFECTIVE_DATE, CREATED_ON, CREATED_BY,RENEWAL_FEE,BASIC_2,TERM)
values('F','PD','BOC','7010',0.02,0.03,0.0125,0.35,500,sysdate,sysdate,user,100,0.02,'L');

ALTER TABLE QUOTATION.FIRE_DH_SCHEDULE_CALC_HISTORY add SC_Renewal_FEE NUMBER(16,2);
ALTER TABLE QUOTATION.FIRE_DH_SCHEDULE_CALC add SC_Renewal_FEE NUMBER(16,2);


CREATE TABLE QUOTATION.DISCOUNT_RATE
 (
  BANK_CODE VARCHAR2(6 BYTE)  NOT NULL ENABLE,
  NO_OF_YEARS NUMBER(6,2),
  RATE NUMBER(8,6), 
  CREATED_ON DATE,
  CREATED_BY VARCHAR2(12),
  UPDATED_ON DATE,
  UPDATED_BY VARCHAR2(12)
   ); 
   
    INSERT ALL
    INTO QUOTATION.DISCOUNT_RATE VALUES ('7010',1,0,SYSDATE,'9641',NULL,NULL)
    INTO QUOTATION.DISCOUNT_RATE VALUES ('7010',2,0.1,SYSDATE,'9641',NULL,NULL)
    INTO QUOTATION.DISCOUNT_RATE VALUES ('7010',3,0.1,SYSDATE,'9641',NULL,NULL)
    INTO QUOTATION.DISCOUNT_RATE VALUES ('7010',4,0.1,SYSDATE,'9641',NULL,NULL)
    INTO QUOTATION.DISCOUNT_RATE VALUES ('7010',5,0.1,SYSDATE,'9641',NULL,NULL)
    INTO QUOTATION.DISCOUNT_RATE VALUES ('7010',6,0.125,SYSDATE,'9641',NULL,NULL)
    INTO QUOTATION.DISCOUNT_RATE VALUES ('7010',7,0.125,SYSDATE,'9641',NULL,NULL)
    INTO QUOTATION.DISCOUNT_RATE VALUES ('7010',8,0.125,SYSDATE,'9641',NULL,NULL)
    INTO QUOTATION.DISCOUNT_RATE VALUES ('7010',9,0.125,SYSDATE,'9641',NULL,NULL)
    INTO QUOTATION.DISCOUNT_RATE VALUES ('7010',10,0.125,SYSDATE,'9641',NULL,NULL)
    INTO QUOTATION.DISCOUNT_RATE VALUES ('7010',11,0.15,SYSDATE,'9641',NULL,NULL)
    INTO QUOTATION.DISCOUNT_RATE VALUES ('7010',12,0.15,SYSDATE,'9641',NULL,NULL)
    
    INTO QUOTATION.DISCOUNT_RATE VALUES ('7010',13,0.15,SYSDATE,'9641',NULL,NULL)
    INTO QUOTATION.DISCOUNT_RATE VALUES ('7010',14,0.15,SYSDATE,'9641',NULL,NULL)
    INTO QUOTATION.DISCOUNT_RATE VALUES ('7010',15,0.15,SYSDATE,'9641',NULL,NULL)
    INTO QUOTATION.DISCOUNT_RATE VALUES ('7010',16,0.15,SYSDATE,'9641',NULL,NULL)
    INTO QUOTATION.DISCOUNT_RATE VALUES ('7010',17,0.15,SYSDATE,'9641',NULL,NULL)
    INTO QUOTATION.DISCOUNT_RATE VALUES ('7010',18,0.15,SYSDATE,'9641',NULL,NULL)
    INTO QUOTATION.DISCOUNT_RATE VALUES ('7010',19,0.15,SYSDATE,'9641',NULL,NULL)
    INTO QUOTATION.DISCOUNT_RATE VALUES ('7010',20,0.15,SYSDATE,'9641',NULL,NULL)
    INTO QUOTATION.DISCOUNT_RATE VALUES ('7010',21,0.15,SYSDATE,'9641',NULL,NULL)
    INTO QUOTATION.DISCOUNT_RATE VALUES ('7010',22,0.15,SYSDATE,'9641',NULL,NULL)
    INTO QUOTATION.DISCOUNT_RATE VALUES ('7010',23,0.15,SYSDATE,'9641',NULL,NULL)
    INTO QUOTATION.DISCOUNT_RATE VALUES ('7010',24,0.15,SYSDATE,'9641',NULL,NULL)
    INTO QUOTATION.DISCOUNT_RATE VALUES ('7010',25,0.15,SYSDATE,'9641',NULL,NULL)
    
    SELECT * FROM dual; 

------SOLAR
 CREATE TABLE QUOTATION.FIRE_DH_SOLAR_RATE
  (SEQ_ID NUMBER(2),
  BANK_CODE VARCHAR2(6 BYTE)  NOT NULL ENABLE,
  MIN_VAL NUMBER(16,2),
  MAX_VAL NUMBER(16,2),
  RATE NUMBER(8,6), 
  CREATED_ON DATE,
  CREATED_BY VARCHAR2(12),
  UPDATED_ON DATE,
  UPDATED_BY VARCHAR2(12),
  ACTIVE VARCHAR2(1 BYTE),
  PRIMARY KEY (SEQ_ID)
  ); 
  
INSERT ALL
INTO QUOTATION.FIRE_DH_SOLAR_RATE VALUES (1,'7010',1,499999,0,sysdate,user,null,null,'Y')
INTO QUOTATION.FIRE_DH_SOLAR_RATE VALUES (2,'7010',500000,1999999,0.005,sysdate,user,null,null,'Y')
INTO QUOTATION.FIRE_DH_SOLAR_RATE VALUES (3,'7010',2000000,5000000,0.004,sysdate,user,null,null,'Y')
SELECT * FROM dual;


---proc for boardraux--------------------------------
drop SEQUENCE QUOTATION.BANCASSU_BORDEREAUX_SEQ 
CREATE SEQUENCE QUOTATION.BANCASSU_BORDEREAUX_SEQ 
    INCREMENT BY 1
    START WITH 1
    MINVALUE 1
    MAXVALUE 999999999999999999999999999
    CACHE 20;
    
 CREATE TABLE QUOTATION.FIRE_DH_BANCASSU_BORDEREAUX
  (SEQ_ID NUMBER(16) NOT NULL ENABLE,
   SC_POLICY_NO VARCHAR2(16 BYTE),
   SC_COMMENCEMENT_DATE DATE,
   SC_NET_PRE NUMBER(16,2),
   SC_RCC NUMBER(10,2),
   SC_TR NUMBER(10,2), 
   CREATED_ON DATE,
   CREATED_BY VARCHAR2(12),
   UPDATED_ON DATE,
   UPDATED_BY VARCHAR2(12),
   PRIMARY KEY (SEQ_ID)
  ); 
  
 
  
 /* QUOTATION.PROC_BANCASSU_BORDEREAUX
  (
 polNumber in varchar2, netPremium in number , startDate in date , srccVal in number , tcVal in number, noOfYears in number, created_by in varchar2, responseVal out number
  )*/

--

  CREATE TABLE QUOTATION.BANCASSU_UPMOTO_QUOTATIONS
   (Q_NO NUMBER NOT NULL ENABLE, 
    Q_REF VARCHAR2(16) NOT NULL ENABLE,
    Q_NAME VARCHAR2(60 BYTE),
    Q_CONTENT  VARCHAR2(120 BYTE),
    Q_DATA BLOB,
    CREATED_BY VARCHAR2(10 BYTE), 
    CREATED_ON DATE,
    Q_FLAG VARCHAR2(1 BYTE),
    P_ACTIVE VARCHAR2(1 BYTE),
    UPDATED_BY VARCHAR2(10), 
    UPDATED_ON DATE,    
   CONSTRAINT PK_BANC_QNO PRIMARY KEY (Q_NO)
   );  
   
  drop SEQUENCE QUOTATION.bacassu_motoquo_seq 
  CREATE SEQUENCE QUOTATION.bacassu_motoquo_seq
    INCREMENT BY 1
    START WITH 1
    MINVALUE 1
    MAXVALUE 999999999999999999999999999
    CACHE 20;
 
  CREATE TABLE QUOTATION.BANCASSU_MOTOQUOREMARKS
   (R_NO NUMBER NOT NULL ENABLE, 
    R_REF VARCHAR2(16) NOT NULL ENABLE,
    R_SLIC VARCHAR2(500 BYTE),
    R_CREATED_BY VARCHAR2(10 BYTE), 
    R_CREATED_ON DATE,
    R_FLAG VARCHAR2(1 BYTE),
    R_BANK VARCHAR2(500 BYTE),
    R_UP_BY_BANK VARCHAR2(10), 
    R_UP_ON_BANK DATE,    
   CONSTRAINT PK_REMARK_RNO PRIMARY KEY (R_NO)
   );
   
 
 
  CREATE SEQUENCE QUOTATION.bacassu_motorespo_seq
    INCREMENT BY 1
    START WITH 1
    MINVALUE 1
    MAXVALUE 999999999999999999999999999
    CACHE 20;  
    
     

  DROP TABLE QUOTATION.FIRE_RATE_LONGTERM_ALL_SUMINSU;

 CREATE TABLE QUOTATION.FIRE_RATE_LONGTERM_ALL_SUMINSU
  (SEQ_ID NUMBER(2),
  BANK_CODE VARCHAR2(6 BYTE)  NOT NULL ENABLE,
  MIN_VAL NUMBER(16,2),
  MAX_VAL NUMBER(16,2),
  RATE NUMBER(8,6), 
  CREATED_ON DATE,
  CREATED_BY VARCHAR2(12),
  UPDATED_ON DATE,
  UPDATED_BY VARCHAR2(12),
  ACTIVE VARCHAR2(1 BYTE),
  PRIMARY KEY (SEQ_ID)
  );  
  
  INSERT ALL
INTO QUOTATION.FIRE_RATE_LONGTERM_ALL_SUMINSU VALUES (1,'7010',1,10000000,0.03,sysdate,user,null,null,'Y')
INTO QUOTATION.FIRE_RATE_LONGTERM_ALL_SUMINSU VALUES (2,'7010',10000001,20000000,0.02,sysdate,user,null,null,'Y')
INTO QUOTATION.FIRE_RATE_LONGTERM_ALL_SUMINSU VALUES (3,'7010',20000001,30000000,0.015,sysdate,user,null,null,'Y')
INTO QUOTATION.FIRE_RATE_LONGTERM_ALL_SUMINSU VALUES (4,'7010',30000001,60000000,0.015,sysdate,user,null,null,'Y')
SELECT * FROM dual;


update QUOTATION.FIRE_DH_SCOPE_OF_COVERS set dh_scope_word ='Electrical Inclusion Clause'
where seq_id =11;


truncate TABLE QUOTATION.FIRE_DH_DEDUCTIBLES
  
select * from QUOTATION.FIRE_DH_DEDUCTIBLES
DROP TABLE QUOTATION.FIRE_DH_DEDUCTIBLES
 CREATE TABLE QUOTATION.FIRE_DH_DEDUCTIBLES
  (SEQ_ID NUMBER(2),
  DH_MIN_VAL NUMBER(10),
  DH_MAX_VAL NUMBER(10),
  DH_OPTION1 VARCHAR2(200 BYTE),
  DH_OPTION2 VARCHAR2(200 BYTE),
  DH_OPTION3 VARCHAR2(200 BYTE),
  DH_OPTION4 VARCHAR2(200 BYTE),
  DH_OPTION5 VARCHAR2(200 BYTE),
  DH_OPTION6 VARCHAR2(200 BYTE),
  DH_OPTION7 VARCHAR2(200 BYTE),
  ACTIVE VARCHAR2(1 BYTE),
  PRIMARY KEY (SEQ_ID)
  );  


INSERT ALL
INTO QUOTATION.FIRE_DH_DEDUCTIBLES VALUES (1,1,2500000,
'Srike, Riot & Civil Commotion - 10% but a maximum of Rs. 100,000.00 on each and every Loss.',
'Terrorism - 10% on each and every loss, but a maximum of Rs. 100,000.00.',
'Cyclone/Strom/Tempest - 10% with a minimum of Rs. 2,500/- on each & every loss.',
'Flood - 10% with a minimum of Rs. 2,500/- on each & every loss.',
'All other natural perils including earthquake with fire & shock - 10% with a minimum of Rs. 2,500/- on each & every loss.',
'Electrical inclusion clause - 10% with a minimum of Rs. 1,000/- on each & every loss.',
'All other causes - 10% with a minimum of Rs. 1,000/- on each & every loss.',
'Y')

INTO QUOTATION.FIRE_DH_DEDUCTIBLES VALUES (2,2500001,5000000,
'Srike, Riot & Civil Commotion - 10% but a maximum of Rs. 100,000.00 on each and every Loss.',
'Terrorism - 10% on each and every loss, but a maximum of Rs. 100,000.00.',
'Cyclone/Strom/Tempest - 10% with a minimum of Rs. 5,000/- on each & every loss.',
'Flood - 10% with a minimum of Rs. 5,000/- on each & every loss.',
'All other natural perils including earthquake with fire & shock - 10% with a minimum of Rs. 5,000/- on each & every loss.',
'Electrical inclusion clause - 10% with a minimum of Rs. 1,500/- on each & every loss.',
'All other causes - 10% with a minimum of Rs. 1,500/- on each & every loss.',
'Y')

INTO QUOTATION.FIRE_DH_DEDUCTIBLES VALUES (3,5000001,7500000,
'Srike, Riot & Civil Commotion - 10% but a maximum of Rs. 100,000.00 on each and every Loss.',
'Terrorism - 10% on each and every loss, but a maximum of Rs. 100,000.00.',
'Cyclone/Strom/Tempest - 10% with a minimum of Rs. 5,000/- on each & every loss.',
'Flood - 10% with a minimum of Rs. 5,000/- on each & every loss.',
'All other natural perils including earthquake with fire & shock - 10% with a minimum of Rs. 5,000/- on each & every loss.',
'Electrical inclusion clause - 10% with a minimum of Rs. 2,000/- on each & every loss.',
'All other causes - 10% with a minimum of Rs. 2,000/- on each & every loss.',
'Y')

INTO QUOTATION.FIRE_DH_DEDUCTIBLES VALUES (4,7500001,10000000,
'Srike, Riot & Civil Commotion - 10% but a maximum of Rs. 100,000.00 on each and every Loss.',
'Terrorism - 10% on each and every loss, but a maximum of Rs. 100,000.00.',
'Cyclone/Strom/Tempest - 10% with a minimum of Rs. 7,500/- on each & every loss.',
'Flood - 10% with a minimum of Rs. 7,500/- on each & every loss.',
'All other natural perils including earthquake with fire & shock - 10% with a minimum of Rs. 7,500/- on each & every loss.',
'Electrical inclusion clause - 10% with a minimum of Rs. 2,500/- on each & every loss.',
'All other causes - 10% with a minimum of Rs. 2,500/- on each & every loss.',
'Y')

INTO QUOTATION.FIRE_DH_DEDUCTIBLES VALUES (5,10000001,15000000,
'Srike, Riot & Civil Commotion - 10% but a maximum of Rs. 100,000.00 on each and every Loss.',
'Terrorism - 10% on each and every loss, but a maximum of Rs. 100,000.00.',
'Cyclone/Strom/Tempest - 10% with a minimum of Rs. 10,000/- on each & every loss.',
'Flood - 10% with a minimum of Rs. 10,000/- on each & every loss.',
'All other natural perils including earthquake with fire & shock - 10% with a minimum of Rs. 10,000/- on each & every loss.',
'Electrical inclusion clause - 10% with a minimum of Rs. 3,000/- on each & every loss.',
'All other causes - 10% with a minimum of Rs. 3,000/- on each & every loss.',
'Y')

INTO QUOTATION.FIRE_DH_DEDUCTIBLES VALUES (6,15000001,20000000,
'Srike, Riot & Civil Commotion - 10% but a maximum of Rs. 100,000.00 on each and every Loss.',
'Terrorism - 10% on each and every loss, but a maximum of Rs. 100,000.00.',
'Cyclone/Strom/Tempest - 10% with a minimum of Rs. 12,500/- on each & every loss.',
'Flood - 10% with a minimum of Rs. 12,500/- on each & every loss.',
'All other natural perils including earthquake with fire & shock - 10% with a minimum of Rs. 12,500/- on each & every loss.',
'Electrical inclusion clause - 10% with a minimum of Rs. 4,000/- on each & every loss.',
'All other causes - 10% with a minimum of Rs. 4,000/- on each & every loss.',
'Y')

INTO QUOTATION.FIRE_DH_DEDUCTIBLES VALUES (7,20000001,25000000,
'Srike, Riot & Civil Commotion - 10% but a maximum of Rs. 100,000.00 on each and every Loss.',
'Terrorism - 10% on each and every loss, but a maximum of Rs. 100,000.00.',
'Cyclone/Strom/Tempest - 10% with a minimum of Rs. 15,000/- on each & every loss.',
'Flood - 10% with a minimum of Rs. 15,000/- on each & every loss.',
'All other natural perils including earthquake with fire & shock - 10% with a minimum of Rs. 15,000/- on each & every loss.',
'Electrical inclusion clause - 10% with a minimum of Rs. 5,000/- on each & every loss.',
'All other causes - 10% with a minimum of Rs. 5,000/- on each & every loss.',
'Y')

INTO QUOTATION.FIRE_DH_DEDUCTIBLES VALUES (8,25000001,30000000,
'Srike, Riot & Civil Commotion - 10% but a maximum of Rs. 100,000.00 on each and every Loss.',
'Terrorism - 10% on each and every loss, but a maximum of Rs. 100,000.00.',
'Cyclone/Strom/Tempest - 10% with a minimum of Rs. 20,000/- on each & every loss.',
'Flood - 10% with a minimum of Rs. 20,000/- on each & every loss.',
'All other natural perils including earthquake with fire & shock - 10% with a minimum of Rs. 20,000/- on each & every loss.',
'Electrical inclusion clause - 10% with a minimum of Rs. 7,500/- on each & every loss.',
'All other causes - 10% with a minimum of Rs. 7,500/- on each & every loss.',
'Y')
SELECT * FROM dual;


/*solar deductibles*/

DROP TABLE QUOTATION.FIRE_SOLAR_DEDUCTIBLES
 CREATE TABLE QUOTATION.FIRE_SOLAR_DEDUCTIBLES
  (SEQ_ID NUMBER(2),
  DH_MIN_VAL NUMBER(10),
  DH_MAX_VAL NUMBER(10),
  DH_OPTION1 VARCHAR2(200 BYTE),
  DH_OPTION2 VARCHAR2(200 BYTE),
  DH_OPTION3 VARCHAR2(200 BYTE),
  DH_OPTION4 VARCHAR2(200 BYTE),
  DH_OPTION5 VARCHAR2(200 BYTE),
  DH_OPTION6 VARCHAR2(200 BYTE),
  DH_OPTION7 VARCHAR2(200 BYTE),
  DH_OPTION8 VARCHAR2(200 BYTE),
  ACTIVE VARCHAR2(1 BYTE),
  PRIMARY KEY (SEQ_ID)
  ); 

  
  INSERT ALL
INTO QUOTATION.FIRE_SOLAR_DEDUCTIBLES VALUES (1,1,400000,
'Srike, Riot & Civil Commotion - 10% but a maximum of Rs. 100,000.00 on each and every Loss.',
'Terrorism - 10% on each and every loss, but a maximum of Rs. 100,000.00.',
'Cyclone/Strom/Tempest - 10% with a minimum of Rs. 2,500/- on each & every loss.',
'Flood - 10% with a minimum of Rs. 2,500/- on each & every loss.',
'All other natural perils including earthquake with fire & shock - 10% with a minimum of Rs. 2,500/- on each & every loss.',
'Electrical inclusion clause - 10% with a minimum of Rs. 1,500/- on each & every loss.',
'Accidental Damage - 10%  or Rs. 1,500/- whichever  is lower',
'All other causes - 10% with a minimum of Rs. 2,500/- on each & every loss.',
'Y')

INTO QUOTATION.FIRE_SOLAR_DEDUCTIBLES VALUES (2,400001,2500000,
'Srike, Riot & Civil Commotion - 10% but a maximum of Rs. 100,000.00 on each and every Loss.',
'Terrorism - 10% on each and every loss, but a maximum of Rs. 100,000.00.',
'Cyclone/Strom/Tempest - 10% with a minimum of Rs. 3,500/- on each & every loss.',
'Flood - 10% with a minimum of Rs. 3,500/- on each & every loss.',
'All other natural perils including earthquake with fire & shock - 10% with a minimum of Rs. 3,500/- on each & every loss.',
'Electrical inclusion clause - 10% with a minimum of Rs. 2,000/- on each & every loss.',
'Accidental Damage - 10%  or Rs. 2,000/- whichever  is lower',
'All other causes - 10% with a minimum of Rs. 3,500/- on each & every loss.',
'Y')

INTO QUOTATION.FIRE_SOLAR_DEDUCTIBLES VALUES (3,2500001,5000000,
'Srike, Riot & Civil Commotion - 10% but a maximum of Rs. 100,000.00 on each and every Loss.',
'Terrorism - 10% on each and every loss, but a maximum of Rs. 100,000.00.',
'Cyclone/Strom/Tempest - 10% with a minimum of Rs. 5,000/- on each & every loss.',
'Flood - 10% with a minimum of Rs. 5,000/- on each & every loss.',
'All other natural perils including earthquake with fire & shock - 10% with a minimum of Rs. 5,000/- on each & every loss.',
'Electrical inclusion clause - 10% with a minimum of Rs. 2,500/- on each & every loss.',
'Accidental Damage - 10%  or Rs.2,500/- whichever  is lower',
'All other causes - 10% with a minimum of Rs. 5,000/- on each & every loss.',
'Y')

SELECT * FROM dual;

/*solar electrical and accidental damge tables*/

DROP TABLE QUOTATION.FIRE_SOLAR_ELECTRICAL_CLAUSE
 CREATE TABLE QUOTATION.FIRE_SOLAR_ELECTRICAL_CLAUSE
  (SEQ_ID NUMBER(2),
  BANK_CODE VARCHAR2(6 BYTE)  NOT NULL ENABLE,
  MIN_VAL NUMBER(16,2),
  MAX_VAL NUMBER(16,2),
  CLA_VAL NUMBER(16,2), 
  CREATED_ON DATE,
  CREATED_BY VARCHAR2(12),
  UPDATED_ON DATE,
  UPDATED_BY VARCHAR2(12),
  ACTIVE VARCHAR2(1 BYTE),
  PRIMARY KEY (SEQ_ID)
  );  
  
  INSERT ALL
INTO QUOTATION.FIRE_SOLAR_ELECTRICAL_CLAUSE VALUES (1,'7010',1,400000,25000,sysdate,user,null,null,'Y')
INTO QUOTATION.FIRE_SOLAR_ELECTRICAL_CLAUSE VALUES (2,'7010',400001,1000000,50000,sysdate,user,null,null,'Y')
INTO QUOTATION.FIRE_SOLAR_ELECTRICAL_CLAUSE VALUES (3,'7010',1000001,2500000,100000,sysdate,user,null,null,'Y')
INTO QUOTATION.FIRE_SOLAR_ELECTRICAL_CLAUSE VALUES (4,'7010',2500001,5000000,200000,sysdate,user,null,null,'Y')
SELECT * FROM dual;


DROP TABLE QUOTATION.FIRE_SOLAR_ACCIDENTAL_CLAUSE
 CREATE TABLE QUOTATION.FIRE_SOLAR_ACCIDENTAL_CLAUSE
  (SEQ_ID NUMBER(2),
  BANK_CODE VARCHAR2(6 BYTE)  NOT NULL ENABLE,
  MIN_VAL NUMBER(16,2),
  MAX_VAL NUMBER(16,2),
  CLA_VAL NUMBER(16,2), 
  CREATED_ON DATE,
  CREATED_BY VARCHAR2(12),
  UPDATED_ON DATE,
  UPDATED_BY VARCHAR2(12),
  ACTIVE VARCHAR2(1 BYTE),
  PRIMARY KEY (SEQ_ID)
  );  
  
  INSERT ALL
INTO QUOTATION.FIRE_SOLAR_ACCIDENTAL_CLAUSE VALUES (1,'7010',1,400000,25000,sysdate,user,null,null,'Y')
INTO QUOTATION.FIRE_SOLAR_ACCIDENTAL_CLAUSE VALUES (2,'7010',400001,1000000,50000,sysdate,user,null,null,'Y')
INTO QUOTATION.FIRE_SOLAR_ACCIDENTAL_CLAUSE VALUES (3,'7010',1000001,2500000,100000,sysdate,user,null,null,'Y')
INTO QUOTATION.FIRE_SOLAR_ACCIDENTAL_CLAUSE VALUES (4,'7010',2500001,5000000,200000,sysdate,user,null,null,'Y')
SELECT * FROM dual;

truncate table QUOTATION.FIRE_DH_SCOPE_OF_COVERS
INSERT ALL
INTO QUOTATION.FIRE_DH_SCOPE_OF_COVERS VALUES (1,'Fire & Lightning','Y')
INTO QUOTATION.FIRE_DH_SCOPE_OF_COVERS VALUES (2,'Strike, Riot & Civil Commotion (Up to the Sum Insured)','Y')
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

select *  from QUOTATION.FIRE_DH_SCOPE_OF_COVERS

/*common response team tables*/


  drop SEQUENCE QUOTATION.bacassu_ticket_seq 
  CREATE SEQUENCE QUOTATION.bacassu_ticket_seq
    INCREMENT BY 1
    START WITH 1
    MINVALUE 1
    MAXVALUE 999999999999999999999999999
    CACHE 20;
    
   
   drop table QUOTATION.BANCASSU_INQU_TICKETS;
    CREATE TABLE QUOTATION.BANCASSU_INQU_TICKETS
    (
    T_REF VARCHAR2(20) NOT NULL ENABLE,
    T_BANK_REMARK VARCHAR2(500 BYTE),
    T_BANK_CODE VARCHAR2(12),
    T_BRANCH_CODE VARCHAR2(12),
    T_BANK_NAME VARCHAR2(100),
    T_BRANCH_NAME VARCHAR2(100),
    T_BANK_EMAIL  VARCHAR2(100),
    T_FLAG VARCHAR2(1 BYTE),
    T_CREATED_BY VARCHAR2(10 BYTE), 
    T_CREATED_ON DATE,
    T_SLIC_REMARK VARCHAR2(500 BYTE),
    T_UP_BY_SLIC VARCHAR2(10), 
    T_UP_ON_SLIC DATE,    
    T_STATUS VARCHAR2(1 BYTE),
   CONSTRAINT PK_T_REF PRIMARY KEY (T_REF)
   );
   
   
   DROP TABLE QUOTATION.BANCASSU_BANK_ITUPDOC
  CREATE TABLE QUOTATION.BANCASSU_BANK_ITUPDOC
   (T_NO NUMBER NOT NULL ENABLE, 
    T_REF VARCHAR2(16) NOT NULL ENABLE,
    T_NAME VARCHAR2(60 BYTE),
    T_CONTENT  VARCHAR2(120 BYTE),
    T_DATA BLOB,
    CREATED_BY VARCHAR2(10 BYTE), 
    CREATED_ON DATE,
    T_FLAG VARCHAR2(1 BYTE),
    T_ACTIVE VARCHAR2(1 BYTE),
    UPDATED_BY VARCHAR2(10), 
    UPDATED_ON DATE,    
   CONSTRAINT PK_T_NO PRIMARY KEY (T_NO)
   );  
   

  drop SEQUENCE QUOTATION.bacassu_bank_ticket_seq 
  CREATE SEQUENCE QUOTATION.bacassu_bank_ticket_seq 
    INCREMENT BY 1
    START WITH 1
    MINVALUE 1
    MAXVALUE 999999999999999999999999999
    CACHE 20;
   );
   
   

    DROP TABLE QUOTATION.BANCASSU_SLIC_ITUPDOC
  CREATE TABLE QUOTATION.BANCASSU_SLIC_ITUPDOC
   (T_NO NUMBER NOT NULL ENABLE, 
    T_REF VARCHAR2(16) NOT NULL ENABLE,
    T_NAME VARCHAR2(60 BYTE),
    T_CONTENT  VARCHAR2(120 BYTE),
    T_DATA BLOB,
    CREATED_BY VARCHAR2(10 BYTE), 
    CREATED_ON DATE,
    T_FLAG VARCHAR2(1 BYTE),
    T_ACTIVE VARCHAR2(1 BYTE),
    UPDATED_BY VARCHAR2(10), 
    UPDATED_ON DATE,    
   CONSTRAINT PK_SLICT_NO PRIMARY KEY (T_NO)
   );  
   

  drop SEQUENCE QUOTATION.bacassu_slic_ticket_seq 
  CREATE SEQUENCE QUOTATION.bacassu_slic_ticket_seq 
    INCREMENT BY 1
    START WITH 1
    MINVALUE 1
    MAXVALUE 999999999999999999999999999
    CACHE 20;
   );
   

   /* policy number creation change-15122021*/
    

    
    ALTER TABLE QUOTATION.FIRE_POLICY_SEQ 
    MODIFY P_SEQ VARCHAR2(10) NOT NULL;
    
    GRANT SELECT ON QUOTATION.BANCASSU_BORDEREAUX_SEQ TO ais;
    GRANT SELECT ON QUOTATION.bacassu_motoquo_seq TO ais;
    GRANT SELECT ON QUOTATION.bacassu_motorespo_seq TO ais;
    GRANT SELECT ON QUOTATION.bacassu_ticket_seq TO ais;
    GRANT SELECT ON QUOTATION.bacassu_bank_ticket_seq TO ais;
    GRANT SELECT ON QUOTATION.bacassu_slic_ticket_seq TO ais;
    
    GRANT EXECUTE ON QUOTATION.PROC_BANCASSU_BORDEREAUX TO ais;
    
   
