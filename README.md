[![Easynvestd](https://www.easynvest.com.br/favicon.ico)](https://www.easynvest.com.br/) 

# Teste de Backend Easynvest 

Este teste é apresentado aos candidatos as vagas de desenvolvimento em .Net para avaliarmos os quesitos técnicos.

# O Desafio

Neste projeto temos um API simples que soluciona o problema das [Torres de Hannoi](https://pt.wikipedia.org/wiki/Torre_de_Han%C3%B3i).

Porém temos dois bugs nesta implementação:
  - A solução apresentada não efetua o último movimento necessário
  - A API não retorna a imagem do estado atual de uma execução

Pedimos aos candidatos que solucionem estes problemas implementando os testes unitários, eles existem para o problema em questão, porém, não estão passando.

# Usando a API

Durante o processo de desenvolvimento é recomendado executar o projeto e consumir as APIs utilizando a ferramenta que você preferir. Abaixo deixamos como consultar a api utilizando o cURL.

Para criar uma nova execução
```
$ curl -X POST "http://localhost:53964/Api/Torre/Hanoi/3"
```

Para verficar o estado da execução criada

```
$ curl -X GET "http://localhost:53964/Api/Torre/Hanoi/54737c01-4c92-4f19-ad20-9ed69a5052c3"
```

E finalmente para obter a imagem da execução

```
$ curl -X GET "http://localhost:53964/Api/Torre/Hanoi/Imagem/54737c01-4c92-4f19-ad20-9ed69a5052c3"
```

Caso você não queira utilizar o **cURL** sugerimos o [Postman](https://www.getpostman.com/)

# Como resolver o teste

Para enviar a sua solução para esses teste é só seguir o [fluxo de fork do github](http://desenvolvimentoparaweb.com/miscelanea/como-fazer-fork-de-um-projeto-no-github/) e enviar o link do Pull Request para nós.

### .Net

O projeto foi criado utilizando o [Visual Studio 2017](https://www.visualstudio.com/pt-br/downloads/) e você pode utilizar o Community para criar a sua solução.
