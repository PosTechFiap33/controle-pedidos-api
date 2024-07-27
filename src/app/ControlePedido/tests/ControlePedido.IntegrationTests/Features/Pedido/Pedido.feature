Feature: Pedido
  Scenario: Cadastrar um pedido vinculado ao cliente com sucesso
    Given que eu iforme o cpf do cliente "71935710010"
    And que eu adicione o produto de id "d0c1c104-4b17-4b24-8195-94d9b1a10b0b"
    And que eu adicione o produto de id "1d93f2c3-f3a7-4e1e-b0d6-3568d2e96e43"
    When eu fizer uma requisicao para gerar o pedido
    Then o status code deve ser 201
    And os dados do pedido estejam validos
    And o cpf vinculado no pedido deve ser "71935710010"
    And o valor do pedido deve ser 50
    And o status do pedido deve ser "Criado"
    And os dados do pagamento devem estar vazios

  Scenario: Cadastrar um pedidos sem vincular cliente com sucesso
    Given que eu adicione o produto de id "d0c1c104-4b17-4b24-8195-94d9b1a10b0b"
    When eu fizer uma requisicao para gerar o pedido
    Then o status code deve ser 201
    And os dados do pedido estejam validos
    And o cpf vinculado no pedido deve ser "CPF não fornecido"
    And o valor do pedido deve ser 30
    And o status do pedido deve ser "Criado"
    And os dados do pagamento devem estar vazios