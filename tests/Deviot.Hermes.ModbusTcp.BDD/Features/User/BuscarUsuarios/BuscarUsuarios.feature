Funcionalidade: Buscar usuários
	Como consumidor de api quero buscar todos usuários

Cenário: Obter todos usuários
Dado que tenho um token de acesso
Quando executar a url via GET
Então a api retornará status code 200
E todos usuários do sistema

Cenário: Erro de autenticação
Quando executar a url via GET
Então a api retornará status code 401
