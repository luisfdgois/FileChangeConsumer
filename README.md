# SQS-File-Notification-Changes

Este serviço é responsável por ler mensagens adicionadas a uma fila do Amazon SQS. Cada mensagem representa um evento de notificação de um serviço de armazenamento de arquivos e está formatada em JSON. Ela contém informações básicas sobre um arquivo, como nome, tamanho e data da notificação.

O serviço lerá cada mensagem na fila e verificará a existência de um arquivo pelo seu nome. Se o arquivo já estiver sido adicionado anteriormente, realizará a alteração apenas se o evento (data da notificação) ocorreu após a última atualização do arquivo. Caso não exista, fará a inserção de um novo arquivo.

### Para executar o serviço:

1. A fila deverá estar previamente criada e com as permissões de usuário IAM já configuradas;
2. Adicione as credenciais (AccessKey e SecretKey) do usuário IAM, bem como o nome da região onde a fila SQS está localizada e o seu nome, ao arquivo appsettings.json;;
3. No diretório da solution .sln, onde está o arquivo docker-compose.yml, execute o seguinte comando:
```bash
docker-compose up -d

```
Será criado um container com um banco MySQL e um outro com o worker que processará as mensagens da fila.

### Json da mensagem:

O body das mensagens inseridas na fila estão em formato json com as seguintes propriedades:
```json
{
  "fileName": "",
  "fileSize": 0,
  "date": "2000-01-01T00:00:00.8090956Z"
}
```
