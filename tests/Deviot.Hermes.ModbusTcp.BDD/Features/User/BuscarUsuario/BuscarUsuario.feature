Funcionalidade: Buscar usuário
	Como consumidor de api quero buscar um usuário específico

Cenário: Obter usuário
Dado que tenho um token de acesso
E que tenho um id de usuário válido
Quando executar a url via GET
Então a api retornará status code 200
E o usuário desejado

Cenário: Usuário não encontrado
Dado que tenho um token de acesso
E que que tenho um id de usuário inválido
Quando executar a url via GET
Então a api retornará status code 404
E a mensagem 'O usuário não foi encontrado'

Cenário: Erro de autenticação
Dado que tenho um id de usuário válido
Quando executar a url via GET
Então a api retornará status code 401
