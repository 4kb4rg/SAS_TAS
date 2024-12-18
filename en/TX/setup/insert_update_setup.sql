IF NOT EXISTS (SELECT TrxID,TrxLnID,AccCode FROM TX_TaxObjectRate WHERE TrxID='[TRXID]' And TrxLnID='[TRXLNID]' And AccCode='[ACCCODE]')
   INSERT INTO TX_TaxObjectRate(TrxID,AccCode,Description,ExpiredDate,ExpiredStatus,Status,CreateDate,UpdateDate,UpdateID) 
   VALUES('[TRXID]','[ACCCODE]','[DESCRIPTION]','[EXPIREDDATE]','[EXPIREDSTATUS]','[STATUS]',GETDATE(),GETDATE(),'[UPDATEID]') 
ELSE
  UPDATE TX_TaxObjectRate 
  SET 
  Description='DESCRIPTION',
  ExpiredDate='[EXPIREDDATE]',
  ExpiredStatus='[EXPIREDSTATUS]',
  Status='[STATUS]',
  UpdateDate=GETDATE(),
  UpdateID='[UPDATEID]'
  WHERE TrxID='[TRXID]' And AccCode='[ACCCODE]'

IF NOT EXISTS (SELECT TrxID,TrxLnID,AccCode FROM TX_TaxObjectRateLn WHERE TrxID='[TRXID]' And TrxLnID='[TRXLNID]' And AccCode='[ACCCODE]')
   INSERT INTO TX_TaxObjectRateLn(TrxID,TrxLnID,AccCode,TaxObject,WRate,WORate,AdditionalNote) 
   VALUES('[TRXID]','[TRXLNID]','[ACCCODE]','[TAXOBJECT]',[WRATE],[WORATE],'[ADDNOTE]') 
ELSE
  UPDATE TX_TaxObjectRateLn 
  SET 
  TaxObject='[TAXOBJECT]',
  WRate=[WRATE],
  WORate=[WORATE],
  AdditionalNote='[ADDNOTE]'
  WHERE TrxID='[TRXID]' and TrxLnID='[TRXLNID]' and AccCode='[ACCCODE]'

