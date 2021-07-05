Funcionalidade: Login de usuário
	Como consumidor de api quero obter um token de acesso

Cenário: Obter token de acesso
Dado que tenho um username e senha válidos
Quando executar a url de login via POST
Então A api retornará um stutus code 200
E um token de acesso valido

Cenário: Negar token de acesso
Dado que tenho um username ou senha invalido
Quando executar a url de login via POST
Então A api retornará um stutus code 404
E uma mensagem de erro: 'Usuário ou senha inválidos.'
