Funcionalidade: Alterar senha usuário
	Como consumidor de api quero alterar a minha senha de usuário

Cenário: Senha alterada com sucesso
Dado que tenho meu token de acesso
E que tenho uma nova senha de usuário
Quando executar a url via PUT
Então a api retornará status code 200
E a mensagem 'O usuário foi atualizado com sucesso'

Cenário: Erro de autorização ao alterar senha de outro usuário
Dado que tenho um token de acesso admin
E que tenho uma nova senha de usuário
Quando executar a url via PUT
Então a api retornará status code 401
E a mensagem 'Não é permitido alterar dados de outro usuário'

Cenário: Erro de validação ao informar senha atual
Dado que tenho meu token de acesso
E que tenho uma nova senha de usuário com a senha atual incorreta
Quando executar a url via PUT
Então a api retornará status code 403
E a mensagem 'Senha atual inválida'


Cenário: Erro de autenticação
E que tenho uma nova senha de usuário
Quando executar a url via PUT
Então a api retornará status code 401