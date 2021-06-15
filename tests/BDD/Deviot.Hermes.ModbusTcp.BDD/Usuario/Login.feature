Funcionalidade: Login de usuário
	Como consumidor de api quero obter um token de acesso.

Cenário: Obter token de acesso
Dado Que digitei um username e senha validos
Então A api retornará um stutus code 200
E um token de acesso valido.

Cenário: Negar token de acesso
Dado Que digitei um username ou senha invalido
Então A api retornará um stutus code 404
E uma mensagem: Usuário ou senha inválidos.