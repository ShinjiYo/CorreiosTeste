Feature: CorreiosFeature
Separei os passos do teste em 3 para um teste mais organizado.
Primeiro para testar caso o CEP não seja encontrado
Segundo para testar um CEP válido e o seu retorno
Terceiro para testar caso uma encomenda não exista
Quarto realizei os testes juntos como estava no documento

@BuscaCEPInvalido
Scenario: Buscando um CEP Inválido
	Given acessei o site dos correios
	When busquei o CEP "80700000"
	Then o sistema retornou a mensagem "Dados não encontrado"

@BuscaCEPValido
Scenario: Buscando um CEP Válido
	Given acessei o site dos correios
	When busquei o CEP "01013-001"
	Then o sistema encontrou o CEP e retornou: "Rua Quinze de Novembro - lado ímpar São Paulo/SP"

	#O Cenário abaixo foi impossível de se fazer totalmente automatizado,
	#pois na parte de buscar a encomenda existe um CAPTCHA que impossibilita a automação
	#então este cenário esta semi-automatizado, pois na hora do captcha tem um sleep para que se insira o código
@BuscaEncomendaInvalida
Scenario: Buscando uma encomenda inválida
	Given acessei o site dos correios
	When busquei a encomenda "SS987654321BR"
	Then o sistema retornou o seguinte status sobre a encomenda "Objeto não encontrado na base de dados dos Correios."

	#O Cenário abaixo foi impossível de se fazer totalmente automatizado,
	#pois na parte de buscar a encomenda existe um CAPTCHA que impossibilita a automação
	#então este cenário esta semi-automatizado, pois na hora do captcha tem um sleep para que se insira o código
@CenarioCompleto
Scenario: Fazendo os tres procedimentos juntos
	Given acessei o site dos correios
	When busquei o CEP "80700000"
	And confirmei que o CEP não existe, com a mensagem "Dados não encontrado", voltei a tela inicial
	And apaguei o cep anterior e busquei o CEP "01013-001"
	And recebi o resultado: "Rua Quinze de Novembro - lado ímpar São Paulo/SP", voltei a tela incial
	And busquei a encomenda "SS987654321BR"
	Then o sistema retornou o seguinte status sobre a encomenda "Objeto não encontrado na base de dados dos Correios."
	# Fechar o site já está nos hooks, para fechar o browser a cada cenário


