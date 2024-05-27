# BD: Guião 6

## Problema 6.1

### *a)* Todos os tuplos da tabela autores (authors);

```
SELECT *
	FROM authors
```

### *b)* O primeiro nome, o último nome e o telefone dos autores;

```
SELECT au_fname , au_lname , phone
	FROM authors
```

### *c)* Consulta definida em b) mas ordenada pelo primeiro nome (ascendente) e depois o último nome (ascendente); 

```
SELECT au_fname , au_lname , phone
	FROM authors
	ORDER BY au_fname,au_lname ASC
```

### *d)* Consulta definida em c) mas renomeando os atributos para (first_name, last_name, telephone); 

```
SELECT au_fname AS first_name , au_lname AS last_name , phone AS telephone
	FROM authors
	ORDER BY au_fname, au_lname ASC
```

### *e)* Consulta definida em d) mas só os autores da Califórnia (CA) cujo último nome é diferente de ‘Ringer’; 

```
SELECT au_fname , au_lname , phone, state
	FROM authors
	WHERE state LIKE '%CA%' and au_lname LIKE '%Ringer%'
	ORDER BY au_fname,au_lname
```

### *f)* Todas as editoras (publishers) que tenham ‘Bo’ em qualquer parte do nome; 

```
SELECT pub_name
	FROM publishers
	WHERE pub_name LIKE '%Bo%';
```

### *g)* Nome das editoras que têm pelo menos uma publicação do tipo ‘Business’; 

```
SELECT DISTINCT pub_name
	FROM dbo.publishers INNER JOIN
                         dbo.titles AS titles_1 ON dbo.publishers.pub_id = titles_1.pub_id
	WHERE type LIKE 'business' 
```

### *h)* Número total de vendas de cada editora; 

```
SELECT publishers.pub_name, SUM (sales.qty) as TotalVendas
	FROM sales
		INNER JOIN titles ON sales.title_id = titles.title_id
		INNER JOIN publishers ON titles.pub_id = publishers.pub_id
	GROUP BY publishers.pub_name
	ORDER BY publishers.pub_name
```

### *i)* Número total de vendas de cada editora agrupado por título; 

```
SELECT publishers.pub_name, titles.title, SUM (sales.qty) as TotalVendas
	FROM sales
		INNER JOIN titles ON sales.title_id = titles.title_id
		INNER JOIN publishers ON titles.pub_id = publishers.pub_id
	GROUP BY publishers.pub_name, titles.title
	ORDER BY publishers.pub_name, titles.title
```

### *j)* Nome dos títulos vendidos pela loja ‘Bookbeat’; 

```
SELECT titles.title
	FROM titles
		INNER JOIN sales ON titles.title_id = sales.title_id
		INNER JOIN stores ON sales.stor_id = stores.stor_id
	WHERE stor_name='Bookbeat'
```

### *k)* Nome de autores que tenham publicações de tipos diferentes; 

```
select au_fname , au_lname, count(*) as types_C
	from authors
		inner join titleauthor on authors.au_id = titleauthor.au_id
		inner join titles on titleauthor.title_id = titles.title_id
	group by au_fname, au_lname
	having count(distinct titles.type) > 1
```

### *l)* Para os títulos, obter o preço médio e o número total de vendas agrupado por tipo (type) e editora (pub_id);

```
SELECT titles.type, AVG(titles.price) as AvgPrice , SUM (sales.qty) as TotalVendas
	FROM sales
		INNER JOIN titles ON sales.title_id = titles.title_id
		INNER JOIN publishers ON titles.pub_id = publishers.pub_id
	GROUP BY titles.type, publishers.pub_id
```

### *m)* Obter o(s) tipo(s) de título(s) para o(s) qual(is) o máximo de dinheiro “à cabeça” (advance) é uma vez e meia superior à média do grupo (tipo);

```
SELECT t1.type
	FROM titles t1
		INNER JOIN (
			SELECT type, MAX(advance) AS max_advance, AVG(advance) AS avg_advance
			FROM titles
			GROUP BY type
		) t2
		ON t1.type = t2.type
	WHERE t2.max_advance > 1.5 * t2.avg_advance;
```

### *n)* Obter, para cada título, nome dos autores e valor arrecadado por estes com a sua venda;

```
SELECT title, au_fname, au_lname, (titles.price * ytd_sales * royalty/100 * royaltyper/100) AS TotalValue
	FROM titles
		INNER JOIN titleauthor ON titles.title_id = titleauthor.title_id
		INNER JOIN authors ON titleauthor.au_id = authors.au_id
	GROUP BY title , au_fname, au_lname, titles.price, ytd_sales, royalty, royaltyper
```

### *o)* Obter uma lista que incluía o número de vendas de um título (ytd_sales), o seu nome, a faturação total, o valor da faturação relativa aos autores e o valor da faturação relativa à editora;

```
SELECT title, ytd_sales, ytd_sales*price AS facturacao, 
	ytd_sales*price*royalty/100 AS auths_revenue, 
	price*ytd_sales-price*ytd_sales*royalty/100 AS publisher_revenue
	FROM titles
```

### *p)* Obter uma lista que incluía o número de vendas de um título (ytd_sales), o seu nome, o nome de cada autor, o valor da faturação de cada autor e o valor da faturação relativa à editora;

```
SELECT title, ytd_sales, au_fname, au_lname,
	(titles.price * ytd_sales * royalty/100 * royaltyper/100) AS Authors_Value,
	(titles.price * ytd_sales) - (titles.price * ytd_sales * royalty)/100 AS Pub_Value
	FROM titles
		INNER JOIN titleauthor ON titleauthor.title_id = titles.title_id
		INNER JOIN authors ON authors.au_id = titleauthor.au_id
	GROUP BY title , au_fname, au_lname, price, ytd_sales, royalty, royaltyper
```

### *q)* Lista de lojas que venderam pelo menos um exemplar de todos os livros;

```
SELECT stor_name FROM stores
		INNER JOIN sales ON stores.stor_id=sales.stor_id
		INNER JOIN titles ON sales.title_id = titles .title_id
	GROUP BY stores.stor_name
	HAVING COUNT(title)=(SELECT COUNT(title_id) FROM titles);
```

### *r)* Lista de lojas que venderam mais livros do que a média de todas as lojas;

```
SELECT stor_name 
	FROM sales
		INNER JOIN stores ON stores.stor_id=sales.stor_id
	GROUP BY stores.stor_name
	HAVING SUM(sales.qty)>(SELECT AVG(sales.qty) FROM sales);
```

### *s)* Nome dos títulos que nunca foram vendidos na loja “Bookbeat”;

```
SELECT title FROM titles
	EXCEPT
	SELECT DISTINCT title 
		FROM titlesINNER JOIN sales ON sales.title_id=titles.title_id
			INNER JOIN stores ON stores.stor_id=sales.stor_id
		WHERE stor_name='Bookbeat'
```	

### *t)* Para cada editora, a lista de todas as lojas que nunca venderam títulos dessa editora; 

```
SELECT pub_name, stor_name 
FROM publishers 
	JOIN stores ON stor_id NOT IN (SELECT stor_id FROM sales INNER JOIN titles ON sales.title_id = titles.title_id) 
	ORDER BY pub_name 
```

## Problema 6.2

### ​5.1

#### a) SQL DDL Script
 
[a) SQL DDL File](ex_6_2_1_ddl.sql "SQLFileQuestion")

#### b) Data Insertion Script

[b) SQL Data Insertion File](ex_6_2_1_data.sql "SQLFileQuestion")

#### c) Queries

##### *a)*

```
SELECT Pname, Ssn, Fname, Lname 
	FROM project
		INNER JOIN works_on ON Pno=Pnumber
		INNER JOIN employee ON Essn=Ssn
```

##### *b)* 

```
SELECT e.Fname, e.Lname 
	FROM Company.Employee e 
		JOIN Company.Employee s ON e.Super_ssn = s.Ssn 
	WHERE s.Fname = 'Carlos' AND s.Minit = 'D' AND s.Lname = 'Gomes';
```

##### *c)* 

```
SELECT Pname, SUM(Hours) AS THours
	FROM project 
		INNER JOIN works_on ON Pnumber=Pno
	GROUP BY Pname
```

##### *d)* 

```
SELECT e.Fname, e.Minit, e.Lname
	FROM employee e
		JOIN works_on w ON e.Ssn = w.Essn
		JOIN project p ON w.Pno = p.Pnumber
		JOIN department d ON p.Dnum = d.Dnumber
	WHERE d.Dnumber = 3 AND w.Hours > 20 AND p.Pname = 'Aveiro Digital'
```

##### *e)* 

```
SELECT Fname, Minit, Lname
	FROM  employee 
		LEFT outer JOIN works_on ON Ssn=Essn
	WHERE Pno IS NULL
```

##### *f)* 

```
SELECT department.Dname, AVG(employee.Salary) AS AvgSalary
	FROM department
		JOIN employee ON department.Dnumber = employee.Dno
	WHERE employee.Sex = 'F'
	GROUP BY department.Dname;
```

##### *g)* 

```
SELECT Fname, Minit, Lname FROM employee
INNER JOIN (
			SELECT Essn, COUNT(Essn) AS quantos FROM dependent
			GROUP BY Essn
			HAVING quantos > 2
			) AS dependentes
ON Ssn=Essn
```

##### *h)* 

```
SELECT Fname, Minit, Lname 
	FROM department
		INNER JOIN employee ON Ssn=Mgr_ssn
		LEFT outer JOIN dependent ON Essn=Ssn
	WHERE Dependent_name IS NULL
```

##### *i)* 

```
SELECT Fname, Minit, Lname, Address FROM employee
INNER JOIN (
			SELECT * 
				FROM project
					INNER JOIN dept_location ON Dnum=Dnumber
				WHERE Dlocation!='Aveiro' AND Plocation='Aveiro'
			) AS PROJECT_LST
ON Dno=Dnum
```

### 5.2

#### a) SQL DDL Script
 
[a) SQL DDL File](ex_6_2_2_ddl.sql "SQLFileQuestion")

#### b) Data Insertion Script

[b) SQL Data Insertion File](ex_6_2_2_data.sql "SQLFileQuestion")

#### c) Queries

##### *a)*

```
SELECT nif, nome
	FROM  fornecedor LEFT outer JOIN encomenda ON fornecedor=nif
	WHERE numero IS NULL
```

##### *b)* 

```
SELECT codigo, nome, AVG(item.unidades) as AvgUniddes
	FROM  item 
		INNER JOIN encomenda ON numEnc=numero
		INNER JOIN produto ON codProd = codigo
	GROUP BY codigo, nome, item.unidades
```


##### *c)* 

```
SELECT AVG(produtos) AS media_produtos
	FROM (SELECT COUNT(codProd) AS produtos 
				FROM encomenda
					INNER JOIN item ON numero = numEnc
					GROUP BY numEnc) AS Produtos
	GROUP BY media_produtos
```


##### *d)* 

```
SELECT fornecedor.nome, produto.nome, SUM(item.unidades) AS quantidades
	FROM produto
		INNER JOIN item ON codProd=codigo
		INNER JOIN encomenda ON numEnc= numero
		INNER JOIN fornecedor ON nif=fornecedor
	GROUP BY fornecedor.nome, produto.nome
```

### 5.3

#### a) SQL DDL Script
 
[a) SQL DDL File](ex_6_2_3_ddl.sql "SQLFileQuestion")

#### b) Data Insertion Script

[b) SQL Data Insertion File](ex_6_2_3_data.sql "SQLFileQuestion")

#### c) Queries

##### *a)*

```
SELECT paciente.nome
	FROM paciente
		INNER JOIN prescricao ON paciente.numUtente = prescricao.numUtente
	WHERE prescricao.numPresc IS NULL;
```

##### *b)* 

```
SELECT medico.especialidade, COUNT(prescricao.numPresc) AS (QUANTOS)
	FROM medico
		INNER JOIN prescricao ON medico.numSNS = prescricao.numMedico
	GROUP BY medico.especialidade;
```


##### *c)* 

```
SELECT f.nome, COUNT(p.numPresc) AS total_presc
	FROM Prescricao.prescricao AS p
		JOIN Prescricao.farmacia AS f ON p.farmacia = f.nome
	GROUP BY f.nome;
```


##### *d)* 

```
SELECT nomeFarmaco
	FROM presc_farmaco
	WHERE numRegFarm = 906;
```

##### *e)* 

```
SELECT farmacia, nome, qtd_farmacos 
	FROM Prescricao.farmaceutica
		INNER JOIN (
					SELECT farmacia, numRegFarm, COUNT(numRegFarm) AS qtd_farmacos FROM
						(SELECT farmacia, numRegFarm FROM Prescricao.presc_farmaco
						INNER JOIN Prescricao.prescricao ON presc_farmaco.numPresc=prescricao.numPresc
						WHERE farmacia IS NOT NULL) AS FARM_SELLED
					GROUP BY farmacia, numRegFarm
					) AS AUX_TABLE
		ON numRegFarm=numReg
```

##### *f)* 

```
SELECT p.nome
	FROM paciente p
		INNER JOIN prescricao pr ON p.numUtente = pr.numUtente
	GROUP BY pr.numUtente
	HAVING COUNT(DISTINCT pr.numMedico) > 1;
```
