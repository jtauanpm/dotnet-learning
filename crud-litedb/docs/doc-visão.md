# CRUD LiteDB

Um simples CRUD de pessoas.

## Equipe e Definição de Papéis

| Membro          | Papel                   | E-mail                            | GitHub               |
| --------------- | ----------------------- | --------------------------------- | -------------------- |
| Jonathan Tauan  | Analista, Desenvolvedor | jonathan.maia.133@ufrn.edu.br     | jtauanpm             |

## Perfis dos Usuários

O sistema poderá ser utilizado por um usuário administrador que terá acesso as operações do CRUD.

| Perfil  | Descrição                                                                                                                                                                                                                                                             |
| ------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Usuário | O usuário poderá criar, editar, remover e vizualizar as pessoas do sistema. |

## Lista de Requisitos Funcionais

| Requisito                                        | Descrição                                                                                                                                 | Ator    |
| ------------------------------------------------ | ----------------------------------------------------------------------------------------------------------------------------------------- | ------- |
| RF001 - Cadastrar pessoa                     | Permitir que o usuário registre pessoas na plataforma fornecendo informações básicas, como nome e idade.                             | Usuário |
| RF002 - Vizualizar pessoa                 | O usuário deve conseguir buscar um usuário e vizualizar suas informações pelo ID.           | Usuário |
| RF003 - Listar pessoas                           | O usuário pode listar todas as pessoas cadastradas.                                                                     | Usuário |
| RF004 - Atualizar pessoa                 | O usuário consegue atualizar os dados de uma pessoa.                                                         | Usuário |
| RF005 - Excluir pessoa               | O usuário pode remover pessoas do sistema.                                             | Usuário |
| RF006 - Cadastrar endereço               | O usuário pode cadastrar endereço para pessoas do sistema.                                             | Usuário |

## Lista de Requisitos Não-Funcionais

| Requisito                                 | Descrição                                                            |
| ----------------------------------------- | -------------------------------------------------------------------- |
| RNF001 - Deve ser acessível via terminal  | Deve funcionar nos terminais dos SOs mais utilizados                 |
| RNF002 - Consultas devem ser eficientes   | O sistema deve executar as consultas rapidamente                     |

## Riscos

Preencher na tabela os riscos identificados para o início do projeto. Essa tabela deve ser atualizada ao final de cada iteração na reunião de acompanhamento.

| Data       | Risco                                                                 | Prioridade | Responsável | Status    | Providência/Solução                                                                     |
| ---------- | --------------------------------------------------------------------- | ---------- | ----------- | --------- | --------------------------------------------------------------------------------------- |
| 27/03/2023 | Não aprendizado das ferramentas utilizadas pelos componentes do grupo | Alta       | Todos       | Vigente   | Reforçar estudos sobre as ferramentas e aulas com a integrante que conhece a ferramenta |
| 27/03/2023 | Ausência por qualquer motivo do cliente                               | Média      | Gerente     | Vigente   | Planejar o cronograma tendo em base a agenda do cliente                                 |
| 27/03/2023 | Divisão de tarefas mal sucedida                                       | Baixa      | Gerente     | Vigente   | Acompanhar de perto o desenvolvimento de cada membro da equipe


### Modelo Conceitual

O modelo conceitual usando o **Mermaid**, pode ser acessado clicando [aqui](./doc-modelos.md).