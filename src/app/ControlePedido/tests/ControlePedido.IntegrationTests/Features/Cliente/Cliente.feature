Feature: Cliente
  Scenario: Cadastrar um cliente com sucesso
    Given que eu iforme o nome "Cliente teste"
    And o email "teste@teste.com"
    And o cpf "71539705099"
    When for feita a requisição para a rota de cadastro
    Then deverá ser retornado o status 201
    And o id do cliente deve ser válido
 
  Scenario: Consultar um cliente cadastrado com sucesso
    Given que eu iforme o nome "Cliente consultado"
    And o email "testeconsulta@teste.com"
    And o cpf "04813536077"
    When for feita a requisição para a rota de cadastro
    And for realizada uma consulta com o id de cliente cadastrado
    Then devera ser retornado os dados do cliente cadastrado

  Scenario: Deve retornar erro em caso de cpf inválido
    Given que eu iforme o nome "Cliente teste"
    And o email "teste@teste.com"
    And o cpf "71539705092"
    When for feita a requisição para a rota de cadastro
    Then deverá ser retornado o status 400
    And a mensagem de erro deve ser "Cpf inválido!"

  Scenario: Deve retornar erro em caso de cpf já cadastrado
    Given que eu iforme o nome "Cliente teste"
    And o email "teste@teste.com"
    And o cpf "71935710010"
    When for feita a requisição para a rota de cadastro
    Then deverá ser retornado o status 400
    And a mensagem de erro deve ser "Cpf já cadastrado no sistema!"

  Scenario: Deve retornar erro em caso de email inválido
    Given que eu iforme o nome "Cliente teste"
    And o email "teste"
    And o cpf "71539705099"
    When for feita a requisição para a rota de cadastro
    Then deverá ser retornado o status 400
    And a mensagem de erro deve ser "Campo Email invalido"

  Scenario: Deve retornar erro em caso de nome inválido
    Given que eu iforme o nome ""
    And o email "teste@teste.com"
    And o cpf "71539705099"
    When for feita a requisição para a rota de cadastro
    Then deverá ser retornado o status 400
    And a mensagem de erro deve ser "Campo Nome obrigatorio"

    