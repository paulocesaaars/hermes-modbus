Funcionalidade: Editar usuário
	Como consumidor de api quero editar um usuário

Cenário: Editar outro usuário com sucesso
Dado que tenho um token de acesso admin
E que tenho um usuário atualizado diferente do meu
Quando executar a url via PUT
Então a api retornará status code 200
E a mensagem 'O usuário foi atualizado com sucesso'

Cenário: Editar meu usuário com sucesso
Dado que tenho um token de acesso normal
E que tenho meu usuário atualizado 
Quando executar a url via PUT
Então a api retornará status code 200
E a mensagem 'O usuário foi atualizado com sucesso'

Cenário: Usuário não encontrado
Dado que tenho um token de acesso admin
E que que tenho um usuário com id inválido
Quando executar a url via PUT
Então a api retornará status code 404
E a mensagem 'O usuário não foi encontrado'

Cenário: Nome de usuário inválido
Dado que tenho um token de acesso admin
E que tenho um usuário com nome inválido
Quando executar a url via PUT
Então a api retornará status code 403
E a mensagem 'O nome de usuário precisar ter somente valores alfanuméricos ou underline'

Cenário: Senha de usuário inválida
Dado que tenho um token de acesso admin
E que tenho um usuário com senha inválida
Quando executar a url via PUT
Então a api retornará status code 403
E a mensagem 'A senha precisar ter somente valores alfanuméricos'

Cenário: Nome de usuário já existente
Dado que tenho um token de acesso admin
E que tenho um usuário com nome já existente
Quando executar a url PUT
Então a api retornará status code 403
E a mensagem 'O nome de usuário informado já existe'

Cenário: Erro de autenticação
Dado que tenho um usuário válido
Quando executar a url via PUT
Então a api retornará status code 401

Cenário: Erro de autorização ao alterar um usuário diferente do meu
Dado que tenho um token de acesso normal
E que tenho um usuário atualizado diferente do meu
Quando executar a url via PUT
Então a api retornará status code 401
E a mensagem 'Não é permitido alterar dados de outro usuário'

Cenário: Erro de autorização ao alterar meu usuário para administrador
Dado que tenho um token de acesso normal
E que tenho meu usuário atualizado para administrador
Quando executar a url via PUT
Então a api retornará status code 401
E a mensagem 'Somente um administrador pode criar ou alterar um usuário administrador'
