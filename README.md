-- INSTRUÇÕES DE CONFIGURAÇÃO
1. No arquivo appsettings.json, defina sua string de conexão com o banco de dados. OBS.: se o banco NÃO for SQL Server, é necessário alterar esse trecho de Program.cs:
![image](https://github.com/user-attachments/assets/08ea3708-b500-406f-87d5-6b98f0c91775)

2. No menu superior do Visual Studio, vá em Ferramentas > Gerenciador de Pacotes do NuGet > Console do gerenciador de pacotes
3. No terminal do console, insira o comando abaixo, ele cria o banco por meio das Migrations
```json
    dotnet ef database update
```
4. No banco de dados, rode o seguinte script sql:

```json
INSERT INTO usuarios (Nome, Login, Email, Perfil, Senha, DataCadastro, DataAtualizacao)
VALUES('Administrador', 'Admin', 'admin@gmail.com', 1, '20eabe5d64b0e216796e834f52d61fd0b70332fc', GETDATE(), GETDATE())
```
Observação: a senha inserida na tabela é 123456, e possui criptografia SHA-1, que converte senhas em hashs de caracteres. Ex.:

![image](https://github.com/user-attachments/assets/fe193243-121a-4d07-96fb-36c883bba622)

Link para conversor: http://www.sha1-online.com/

-- FIM DE CONFIGURAÇÃO

