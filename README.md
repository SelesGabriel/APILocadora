1 - Banco utilizado: MySQL (Banco online do herokuapp)

2 - API está online também no link: https://locadoraapi.herokuapp.com/api/

3 - Para facilitar os exemplos JSON estarão logo abaixo:

https://locadoraapi.herokuapp.com/api/cliente 
{
	"Nome":"",
	"Sobrenome":"",
	"CPF":""
} (POST)

https://locadoraapi.herokuapp.com/api/filme 
{
	"Nome":"",
	"Categoria":""
} (POST)

https://locadoraapi.herokuapp.com/api/locacao 
{
    "FilmeId": 2,
    "ClienteId": 13,
    "QtdDias":10 
} (POST)

https://locadoraapi.herokuapp.com/api/locacao/1
{
    "Devolveu":true
} (PUT)
