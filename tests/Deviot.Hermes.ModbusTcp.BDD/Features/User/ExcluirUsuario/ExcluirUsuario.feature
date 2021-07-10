Funcionalidade: Excluir usuário
	Como consumidor de api quero excluir um usuário

Cenário: Excluir usuário com sucesso
Dado que tenho um token de acesso admin
E que tenho um id de usuário válido
Quando executar a url via DELETE
Então a api retornará status code 200
E a mensagem 'O usuário foi deletado com sucesso'

Cenário: Usuário não encontrado
Dado que tenho um token de acesso admin
E que que tenho um id de usuário inválido
Quando executar a url via DELETE
Então a api retornará status code 404
E a mensagem 'O usuário não foi encontrado'

Cenário: Erro de autenticação
Dado que tenho um usuário válido
Quando executar a url via DELETE
Então a api retornará status code 401

Cenário: Erro de autorização ao deletar um usuário
Dado que tenho um token de acesso normal
E que tenho um usuário válido
Quando executar a url via PUT
Então a api retornará status code 401
E a mensagem 'Somente um administrador pode criar ou deletar um usuário'