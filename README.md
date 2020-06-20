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


Regras:

Filmes;

Os campos Nome e categoria são de preenchimento obrigatório.

Não é possível deletar um filme caso ele esteja atrelado à alguma locação. 

Não é possível alterar o Id de um filme. 


Cliente;

Os campos Nome, sobrenome e CPF são de preenchimento obrigatório. 

Necessário colocar um cpf válido, seja com pontuação ou sem pontuação. 

Não é possível colocar um cliente repetido (validação por CPF). 

Não é possível deletar um cliente caso ele esteja atrelado à alguma locação. 

Ao alterar um cliente, o CPF não pode ser igual à algum outro que está cadastrado. 

Locacao;

Os campos FilmeId, ClienteId e QtdDias são de preenchimento obrigatório.

O campo DataLocacao vem como valor default a data atual em que o filme foi locado.

Para locar um filme, é necessário colocar o Id de um cliente existente e um Id de um filme existente.

O campo QtdDias representa a quantidade de dias que o cliente vai locar o filme.

O alerta de devolução vai de acordo com a quantidade de dias faltantes para a devolução.

Para alterar o estado da locação, só é possível alterar se o cliente devolveu ou não o filme. Caso tenha devolvido, 
o alerta de devolução informa que o cliente devolveu o filme.

Não é possível deletar uma locação caso a mesma ainda esteja ativa, ou seja, o cliente ainda não devolveu o filme.
