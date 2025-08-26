  select * from QUOTATION.FIRE_DH_PROPOSAL_ENTRY
  order by dh_entered_on desc;
   
  ALTER TABLE QUOTATION.FIRE_DH_PROPOSAL_ENTRY  
  MODIFY (dh_cfyesr2 VARCHAR2(1),dh_cfyesr3 VARCHAR2(1),dh_cfyesr4 VARCHAR2(1));
  
  ALTER TABLE QUOTATION.FIRE_DH_PROPOSAL_ENTRY    
  ADD (LAND_PHONE VARCHAR2(16), ADDED_DEDUCT VARCHAR2(200),DH_VAL_BANKFAC NUMBER(16,2));
  
  ALTER TABLE QUOTATION.FIRE_DH_PROPOSAL_ENTRY    
  ADD DH_VAL_BANKFAC NUMBER(16,2);

--changes for table
truncate TABLE QUOTATION.FIRE_DH_SCOPE_OF_COVERS
  
select * from QUOTATION.FIRE_DH_SCOPE_OF_COVERS

INSERT ALL
INTO QUOTATION.FIRE_DH_SCOPE_OF_COVERS VALUES (1,'Fire & Lightning','Y')
INTO QUOTATION.FIRE_DH_SCOPE_OF_COVERS VALUES (2,'Malicious Damage','Y')
INTO QUOTATION.FIRE_DH_SCOPE_OF_COVERS VALUES (3,'Explosion','Y')
INTO QUOTATION.FIRE_DH_SCOPE_OF_COVERS VALUES (4,'Cyclone, Storm, Tempest','Y')
INTO QUOTATION.FIRE_DH_SCOPE_OF_COVERS VALUES (5,'Flood','Y')
INTO QUOTATION.FIRE_DH_SCOPE_OF_COVERS VALUES (6,'Earthquake with Fire & Shock','Y')
INTO QUOTATION.FIRE_DH_SCOPE_OF_COVERS VALUES (7,'Natural Disaster (Including Tsunami, Tidal Waves, Volcanic Eruption, Tornado, Hurricane, Typhoon, Thunderstorm, Hailstorm, Windstorm Rainstorm due to an atmospheric disturbance so designated by the Meteorological Department subject to exceptions/exclusions mentioned under the Cyclone, Storm and Tempest under the standard fire policy).','Y')
INTO QUOTATION.FIRE_DH_SCOPE_OF_COVERS VALUES (8,'Aircraft Damage','Y')
INTO QUOTATION.FIRE_DH_SCOPE_OF_COVERS VALUES (9,'Impact Damage','Y')
INTO QUOTATION.FIRE_DH_SCOPE_OF_COVERS VALUES (10,'Bursting or Overflowing of Water Tanks, Apparatus or pipes','Y')
INTO QUOTATION.FIRE_DH_SCOPE_OF_COVERS VALUES (11,'Electrical clause to be granted with maximum limit of 10% on building value/sum insured','Y')
INTO QUOTATION.FIRE_DH_SCOPE_OF_COVERS VALUES (12,'Strike, Riot & Civil Commotion','Y')
INTO QUOTATION.FIRE_DH_SCOPE_OF_COVERS VALUES (13,'Terrorism','Y')
SELECT * FROM dual;


truncate TABLE QUOTATION.FIRE_DH_DEDUCTIBLES
  
select * from QUOTATION.FIRE_DH_DEDUCTIBLES


INSERT ALL
INTO QUOTATION.FIRE_DH_DEDUCTIBLES VALUES (1,1000000,2500000,
'Srike, Riot & Civil Commotion - 10% with a maximum of Rs.100,000.00 on Each and Every Loss.',
'Terrorism - 10% with a maximum of Rs.100,000.00 on Each and Every Loss.',
'All Natural Perils including Cyclone/Strom/Tempest/Flood & Earthquake - 10% with a minimum of Rs.2,500/- on each & every loss.',
'All other Perils - 10% with a minimum of Rs.1,000/- on each & every loss.',
'Y')
INTO QUOTATION.FIRE_DH_DEDUCTIBLES VALUES (2,2500000,5000000,
'Srike, Riot & Civil Commotion - 10% with a maximum of Rs.100,000.00 on Each and Every Loss.',
'Terrorism - 10% with a maximum of Rs.100,000.00 on Each and Every Loss.',
'All Natural Perils including Cyclone/Strom/Tempest/Flood & Earthquake - 10% with a minimum of Rs.5,000/- on each & every loss.',
'All other Perils - 10% with a minimum of Rs.1,500/- on each & every loss.',
'Y')

INTO QUOTATION.FIRE_DH_DEDUCTIBLES VALUES (3,5000000,7500000,
'Srike, Riot & Civil Commotion - 10% with a maximum of Rs.100,000.00 on Each and Every Loss.',
'Terrorism - 10% with a maximum of Rs.100,000.00 on Each and Every Loss.',
'All Natural Perils including Cyclone/Strom/Tempest/Flood & Earthquake - 10% with a minimum of Rs. 5,000/- on each & every loss.',
'All other Perils - 10% with a minimum of Rs.2,000/- on each & every loss.',
'Y')

INTO QUOTATION.FIRE_DH_DEDUCTIBLES VALUES (4,7500000,10000000,
'Srike, Riot & Civil Commotion - 10% with a maximum of Rs.100,000.00 on Each and Every Loss.',
'Terrorism - 10% with a maximum of Rs.100,000.00 on Each and Every Loss.',
'All Natural Perils including Cyclone/Strom/Tempest/Flood & Earthquake - 10% with a minimum of Rs.7,500/- on each & every loss.',
'All other Perils - 10% with a minimum of Rs.2,500/- on each & every loss.',
'Y')

INTO QUOTATION.FIRE_DH_DEDUCTIBLES VALUES (5,10000000,15000000,
'Srike, Riot & Civil Commotion - 10% with a maximum of Rs.100,000.00 on Each and Every Loss.',
'Terrorism - 10% with a maximum of Rs.100,000.00 on Each and Every Loss.',
'All Natural Perils including Cyclone/Strom/Tempest/Flood & Earthquake - 10% with a minimum of Rs.10,000/- on each & every loss.',
'All other Perils - 10% with a minimum of Rs.3,000/- on each & every loss.',
'Y')

INTO QUOTATION.FIRE_DH_DEDUCTIBLES VALUES (6,15000000,20000000,
'Srike, Riot & Civil Commotion - 10% with a maximum of Rs.100,000.00 on Each and Every Loss.',
'Terrorism - 10% with a maximum of Rs.100,000.00 on Each and Every Loss.',
'All Natural Perils including Cyclone/Strom/Tempest/Flood & Earthquake - 10% with a minimum of Rs.12,500/- on each & every loss',
'All other Perils - 10% with a minimum of Rs.4,000/- on each & every loss.',
'Y')

INTO QUOTATION.FIRE_DH_DEDUCTIBLES VALUES (7,20000000,25000000,
'Srike, Riot & Civil Commotion - 10% with a maximum of Rs.100,000.00 on Each and Every Loss.',
'Terrorism - 10% with a maximum of Rs.100,000.00 on Each and Every Loss.',
'All Natural Perils including Cyclone/Strom/Tempest/Flood & Earthquake - 10% with a minimum of Rs.15,000/- on each & every loss.',
'All other Perils - 10% with a minimum of Rs.5,000/- on each & every loss.',
'Y')

INTO QUOTATION.FIRE_DH_DEDUCTIBLES VALUES (8,25000000,30000000,
'Srike, Riot & Civil Commotion - 10% with a maximum of Rs.100,000.00 on Each and Every Loss.',
'Terrorism - 10% with a maximum of Rs.100,000.00 on Each and Every Loss.',
'All Natural Perils including Cyclone/Strom/Tempest/Flood & Earthquake - 10% with a minimum of Rs.20,000/- on each & every loss.',
'All other Perils - 10% with a minimum of Rs.7,500/- on each & every loss.',
'Y')
SELECT * FROM dual;

insert into QUOTATION.FIRE_AGENT_INFO values('780584','PB','192','7135','82022680',user,sysdate,'Y');
INSERT INTO QUOTATION.BANCASU_RATE VALUES ('F','PD','PB','7135',0.04,0.03,0.0125,0.35,500,SYSDATE,SYSDATE,USER);



 INSERT ALL
INTO QUOTATION.BANK_SALES_OFFICER VALUES ('8264','7135','PB','Prasath Madushanka','Sales Officer',
'0777794707','prasathh@srilankainsurance.com', '9641',sysdate,'A')
INTO QUOTATION.BANK_SALES_OFFICER VALUES ('9458','7135','PB','Nishan Edirisinghe','Underwriter',
'0762037876','nishane@srilankainsurance.com', '9641',sysdate,'A')

SELECT * FROM dual;

---new update --10/11/2020

ALTER TABLE QUOTATION.BANK_SALES_OFFICER    
  ADD ONLY_CONTACTS VARCHAR2(2);


SELECT * FROM QUOTATION.FIRE_DH_CONTACT_EMAILS
SELECT * FROM QUOTATION.BANK_SALES_OFFICER
select * from GENPAY.BNKBRN 

INSERT INTO QUOTATION.BANCASU_RATE VALUES ('F','PD','BOC','7010',0.04,0.03,0.0125,0.35,500,SYSDATE,SYSDATE,USER);

SELECT *  FROM QUOTATION.BANCASU_RATE@LIVE
--select * from SLISCHOOL.CLAIM_MAST
--select * from SLISCHOOL.POLICY_MAST 

--select count(*) from QUOTATION.FIRE_AGENT_INFO where bank_code = '7010' and active='Y'

INSERT ALL
INTO QUOTATION.FIRE_DH_CONTACT_EMAILS VALUES ('0006','7010','BOC','Ruween H.N.','ruweenn@srilankainsurance.com', 'Y','9641',sysdate,'A')
INTO QUOTATION.FIRE_DH_CONTACT_EMAILS VALUES ('0006','7135','PEOPLES BANK','Ruween H.N.','ruweenn@srilankainsurance.com', 'Y','9641',sysdate,'A')
INTO QUOTATION.FIRE_DH_CONTACT_EMAILS VALUES ('0007','7010','BOC','Pasindu Tammitge','pasindut@srilankainsurance.com', 'Y','9641',sysdate,'A')
SELECT * FROM dual; 