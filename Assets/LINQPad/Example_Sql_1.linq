<Query Kind="Expression">
  <Connection>
    <ID>0e1f5128-f640-4714-95d8-4c0323425aef</ID>
    <Persist>true</Persist>
    <Server>LORENTZ\MSSQLSER_INST_1</Server>
    <Database>AdventureWorks2012</Database>
  </Connection>
</Query>

// Set the Language dropdown to C# Expression and Connection to LORENTZ\MSSQLSER_INST_1.AdventureWorks2012
//from p in Persons select p

//from a in Addresses select a
from a in Addresses orderby a.City select a

