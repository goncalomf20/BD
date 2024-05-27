# BD: Guião 8


## ​8.1. Complete a seguinte tabela.
Complete the following table.

| #    | Query                                                                                                      | Rows  | Cost  | Pag. Reads | Time (ms) | Index used | Index Op.            | Discussion |
| :--- | :--------------------------------------------------------------------------------------------------------- | :---- | :---- | :--------- | :-------- | :--------- | :------------------- | :--------- |
| 1    | SELECT * from Production.WorkOrder                                                                         | 72591 | 0.474 | 552        | 16        |[PK_WorkOrder_WorkOrderID]| Clustered Index Scan |            |
| 2    | SELECT * from Production.WorkOrder where WorkOrderID=1234                                                  | 1     |0.00328| 220        | 0         |[PK_WorkOrder_WorkOrderID] | Clustered Index Seek |            |
| 3.1  | SELECT * FROM Production.WorkOrder WHERE WorkOrderID between 10000 and 10010                               | 11    |0.003295|  26       | 0         |[PK_WorkOrder_WorkOrderID]| Clustered Index Seek |            |
| 3.2  | SELECT * FROM Production.WorkOrder WHERE WorkOrderID between 1 and 72591                                   | 72591 |0.474   |  746      | 18        |[PK_WorkOrder_WorkOrderID]| Clustered Index Seek |            |
| 4    | SELECT * FROM Production.WorkOrder WHERE StartDate = '2012-05-14'                                          | 55    |0.474   |  1915     | 7         |[PK_WorkOrder_WorkOrderID]| Clustered Index Scan |            |
| 5    | SELECT * FROM Production.WorkOrder WHERE ProductID = 757                                                   | 9     |0.034   |  240      | 3         |[PK_WorkOrder_WorkOrderID]| Clustered Key Lookup     |            |
| 6.1  | SELECT WorkOrderID, StartDate FROM Production.WorkOrder WHERE ProductID = 757                              | 9     |0.00329 | 26        | 0         |[IX_WorkOrder_ProductID]  | Non Clustered Index Seek |            |
| 6.2  | SELECT WorkOrderID, StartDate FROM Production.WorkOrder WHERE ProductID = 945                              | 1105  |0.00602 | 30        | 1         |[IX_WorkOrder_ProductID]  | Non Clustered Index Seek |            |
| 6.3  | SELECT WorkOrderID FROM Production.WorkOrder WHERE ProductID = 945 AND StartDate = '2011-12-04'            | 1     |0.00623 | 32        | 0         |[IX_WorkOrder_ProductID]  | Non Clustered Index Seek |            |
| 7    | SELECT WorkOrderID, StartDate FROM Production.WorkOrder WHERE ProductID = 945 AND StartDate = '2011-12-04' | 1     |0.00623 | 36        | 0         |[IX_WorkOrder_ProductID]  | Non Clustered Index Seek |            |
| 8    | SELECT WorkOrderID, StartDate FROM Production.WorkOrder WHERE ProductID = 945 AND StartDate = '2011-12-04' | 1     |0,00328 | 60        | 0         |Composite (ProductID, StartDate)  | Non Clustered Index Seek |            |

## ​8.2.

### a)

```
ALTER TABLE mytemp ADD CONSTRAINT my_temp_pk PRIMARY KEY CLUSTERED (rid);
```

### b)

```
Percentagem de fragmentação dos índices: 98,58%
Percentagem de ocupação das páginas dos índices: 68,35%
```

### c)

```
CREATE UNIQUE CLUSTERED INDEX  IxRid_c1 ON mytemp(rid) WITH (FILLFACTOR = 65, PAD_INDEX = ON)
Tempo de inserção(ms) : 140810

CREATE UNIQUE CLUSTERED INDEX  IxRid_c2 ON mytemp(rid) WITH (FILLFACTOR = 80, PAD_INDEX = ON)
Tempo de inserção(ms) : 136992

CREATE UNIQUE CLUSTERED INDEX  IxRid_c3 ON mytemp(rid) WITH (FILLFACTOR = 90, PAD_INDEX = ON)
Tempo de inserção(ms) : 167983
```

### d)

```
CREATE TABLE mytemp (
    rid BIGINT IDENTITY (1, 1) NOT NULL,
    at1 INT NULL,
    at2 INT NULL,
    at3 INT NULL,
    lixo varchar(100) NULL
);
SET IDENTITY_INSERT mytemp ON ;

Tempo de inserção(ms) : 143989 (-> FILLFACTOR = 65)
Tempo de inserção(ms) : 140390 (-> FILLFACTOR = 80)
Tempo de inserção(ms) : 140156 (-> FILLFACTOR = 90)
```

### e)

```
CREATE NONCLUSTERED INDEX IxAt1 ON mytemp(at1)
CREATE NONCLUSTERED INDEX IxAt2 ON mytemp(at2)
CREATE NONCLUSTERED INDEX IxAt3 ON mytemp(at3)
CREATE NONCLUSTERED INDEX IxAt1 ON mytemp(at1)

Com indexes os tempos de inserção são mais demorados pois a inserção não é eficiente, é lenta.
```

## ​8.3.

```
i) CREATE UNIQUE CLUSTERED INDEX IxEmployeeSsn ON Company.Employee(Ssn);

ii) CREATE CLUSTERED INDEX IxEmployeeName ON Company.Employee(Fname, Lname);

iii) CREATE UNIQUE CLUSTERED INDEX IxDeptNumber ON Company.Department(Dnumber);
	 CREATE NONCLUSTERED INDEX IxEmpDept ON Company.Employee(Dno)

iv) CREATE UNIQUE CLUSTERED INDEX IxEmployeeSsn ON Company.Employee(Ssn);
	CREATE UNIQUE CLUSTERED INDEX IxProjNumber ON Company.Project(Pnumber);
	CREATE NONCLUSTERED INDEX IxEWorsOnP ON Company.Works_On(Essn, Pno);

v) CREATE UNIQUE CLUSTERED INDEX IxEmployeeSsn ON Company.Employee(Ssn);
	CREATE UNIQUE CLUSTERED INDEX IxEDependent ON Company.Dependent(Essn);

vi) CREATE UNIQUE CLUSTERED INDEX IxDeptNumber ON Company.Department(Dnumber);
	CREATE NONCLUSTERED INDEX IxDeptProj ON Company.Project(Dnum)
```
