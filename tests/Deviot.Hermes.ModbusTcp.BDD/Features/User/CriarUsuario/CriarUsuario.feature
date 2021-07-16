Funcionalidade: Criar usuário
	Como consumidor de api quero criar um usuário

Cenário: Criar usuário com sucesso
Dado que tenho um token de acesso admin
E que tenho um usuário válido
Quando executar a url via POST
Então a api retornará status code 201
E a mensagem 'O usuário foi criado com sucesso'

Cenário: Nome de usuário inválido
Dado que tenho um token de acesso admin
E que tenho um usuário com nome inválido
Quando executar a url via POST
Então a api retornará status code 400
E a mensagem 'O nome de usuário precisar ter somente valores alfanuméricos ou underline'

Cenário: Senha de usuário inválida
Dado que tenho um token de acesso admin
E que tenho um usuário com senha inválida
Quando executar a url via POST
Então a api retornará status code 400
E a mensagem 'A senha precisar ter somente valores alfanuméricos'

Cenário: Nome de usuário já existente
Dado que tenho um token de acesso admin
E que tenho um usuário com nome já existente
Quando executar a url via POST
Então a api retornará status code 400
E a mensagem 'O nome de usuário informado já existe'

Cenário: Erro de autenticação
Dado que tenho um usuário válido
Quando executar a url via POST
Então a api retornará status code 401

Cenário: Erro de autorização
Dado que tenho um token de acesso normal
E que tenho um usuário válido
Quando executar a url via POST
Então a api retornará status code 403
E a mensagem 'Somente um administrador pode criar ou deletar um usuário'
