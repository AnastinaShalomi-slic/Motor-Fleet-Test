SELECT CN.FDREFNO,
       CN.PLACE,
       FC.FDSTAT,
       FC.FDNAME,
       FC.FDADD1,
       FC.FDADD2,
       FC.FDADD3,
       FC.FDADD4,
       CN.FDTYPE,
       CN.DAYS,
       CN.TIME,
       TO_CHAR(CN.FDAT, 'YYYY/MM/DD') AS TEMP_FDAT,
       CN.FDAT,
       
       CN.MAKE,
       CN.CHAS,
       CN.HPOW,
       CN.USED,
       CN.RCC,
       CN.TR,
       CN.FLOOD,
       CN.POLNO,
       CN.VAL,
       CN.PVID,
       CN.PVRG,
       CN.EEPF,
       CN.PPRM,
       CN.PPROP,
       CN.PSLTAX,
       CN.PLTAX,
       CN.PSDTAX,
       CN.PTRAN,
       CN.INSPECT
  FROM FLEET.COVER_NOTE CN
 INNER JOIN FLEET.MFCLIENT FC
    ON CN.FDCLID = FC.FDCLID
   AND CN.FDLOC = FC.FDSUFF
 WHERE CN.FDAT IN ((SELECT MAX(FDAT)
                     FROM FLEET.COVER_NOTE
                    WHERE TRIM(FDVNO1) = TRIM('KS')
                      AND TRIM(FDVNO2) = TRIM('2108')))
   AND TRIM(FDVNO1) = TRIM('KS')
   AND TRIM(FDVNO2) = TRIM('2108')

LO 4952 
CAT 1142
KS 3202 TEST

select * from  FLEET.COVER_NOTE
WHERE FDAT IN ((SELECT MAX(FDAT)
                     FROM FLEET.COVER_NOTE
                    WHERE TRIM(FDVNO1) = TRIM('KS')
                      AND TRIM(FDVNO2) = TRIM('2108')))
   AND TRIM(FDVNO1) = TRIM('KS')
   AND TRIM(FDVNO2) = TRIM('2108')
order by fdat desc
select * from GENPAY.BRANCH Where  BRCOD = "& brcode &"  "

select * from  FLEET.MFCLIENT Where FDCLID='"& mpclid &"' and FDSUFF="& mploc &"
