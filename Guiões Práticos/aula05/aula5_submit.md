# BD: Guião 5


## ​Problema 5.1
 
### *a)*

```
(π Pname, Pnumber  (project) ⨝Pnumber = Pno works_on) ⨝ Essn = Ssn (π Fname,Lname,Ssn (employee))
```


### *b)* 

```
π Fname,Minit,Lname,employee.Ssn (employee ⨝ employee.Super_ssn=supervisor.Ssn (ρ supervisor (π Ssn (σ (Fname='Carlos' ∧ Minit='D' ∧ Lname='Gomes') (employee)))))
```


### *c)* 

```
γ Pname; THours←sum(Hours) (project⨝ Pnumber=Pno works_on)
```


### *d)* 

```
(π Fname, Minit, Lname σ Dnumber=3 ∧ Hours>20 ∧ Pname='Aveiro Digital' (employee⨝ Ssn=Essn (department⨝ Dnumber=Dnum (project⨝ Pnumber=Pno works_on))))
```


### *e)* 

```
π Fname,Minit,Lname σ Pnumber=null (employee⟕ Ssn=Essn (project⨝ Pnumber=Pno works_on))
```


### *f)* 

```
γ Dname; avg(Salary) -> AvgSalary σ Sex = 'F' (department ⨝ Dnumber = Dno (employee))
```


### *g)* 

```
π Fname, Minit , Lname σ quantos > 2 (γ Fname, Minit , Lname ; count(Essn) -> quantos (dependent ⨝ Ssn = Essn employee))
```


### *h)* 

```
(π Fname,Minit,Lname σDependent_name=null (department⨝ Ssn=Mgr_ssn (employee⟕dependent)))
```


### *i)* 

```
π Ssn, Fname, Lname, Address ( σ Dlocation!='Aveiro' (dept_location ⟗ (department ⨝ Dnumber=Dno (employee ⨝ Ssn = Essn (π Essn ( works_on ⨝ Pno=Pnumber ( π Pnumber (σ Plocation='Aveiro' (project)))))))))
```


## ​Problema 5.2

### *a)*

```
(σ numero=null (fornecedor⟕ fornecedor=nif encomenda))
```

### *b)* 

```
(γ codProd; avg(unidades) -> media_unidades (encomenda⨝ numero=numEnc item))
```


### *c)* 

```
γ avg(produtos)->media_produtos ( γ numEnc; count(codProd) -> produtos (encomenda⨝ numero=numEnc item))
```


### *d)* 

```
γ fornecedor.nome, produto.nome ; sum(item.unidades)-> unidades (produto⨝ codProd=codigo item⨝ numEnc=numero encomenda⨝ nif=fornecedor fornecedor)
```


## ​Problema 5.3

### *a)*

```
π nome (σ numPresc=null (paciente ⟗ paciente.numUtente=prescricao.numUtente (prescricao)))
```

### *b)* 

```
(γ especialidade; count(numPresc) -> quantos (medico ⨝ numSNS = numMedico prescricao)) 
```


### *c)* 

```
(γ nome; count(numPresc) -> quantos (farmacia ⨝ nome = farmacia prescricao)) 
```


### *d)* 

```
π nomeFarmaco (presc_farmaco - σ numRegFarm = 906 (presc_farmaco))
```

### *e)* 

```
(γ farmacia , nome; sum(quantos)-> quantidade (σ farmacia ≠ null (π farmacia,numPresc (prescricao)) ⨝ prescricao.numPresc =    presc_farmaco.numPresc γ numPresc,nome; count(numPresc)-> quantos (farmaceutica ⨝ numReg = numRegFarm presc_farmaco)))
```

### *f)* 

```
π nome (σ quantos > 1 (γ prescricao.numUtente; count(numMedico)-> quantos (prescricao)) ⨝ prescricao.numUtente = paciente.numUtente paciente)
```
