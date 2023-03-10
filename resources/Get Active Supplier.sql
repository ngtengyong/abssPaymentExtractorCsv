select 
cd.CardIdentification
,cd.Name
,count(1) as PayCount
from 
SupplierPayments sup
inner join Cards cd on sup.CardRecordID = cd.CardRecordID
inner join Accounts ac on sup.IssuingAccountID = ac.AccountID
where sup.Date >= '2018-01-01' and ac.AccountNumber = '1-2110' and cd.IdentifierID like '%P%'
group by 
cd.CardIdentification
,cd.Name
order by count(1) desc