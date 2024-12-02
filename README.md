<p align="center">
  <a href="https://dotnet.microsoft.com/" target="blank"><img src="https://upload.wikimedia.org/wikipedia/commons/e/ee/.NET_Core_Logo.svg" width="120" alt=".NET Logo" /></a>
</p>

<div class="container">
  <h1>🌟 EA-CustomersService - Microservice for Customer Management</h1>
  <p>
    This project is a microservice that is part of the <strong>EA eCommerce</strong> system. It provides a <strong>Customer Management API</strong> 
    built with <strong>.NET</strong>, following <strong>CQRS</strong>, <strong>Clean Architecture</strong>, and <strong>Clean Code</strong> principles. 
    The service communicates using RPC messaging via <strong>RabbitMQ</strong> and <strong>EasyNetQ</strong>.
  </p>

  <h2>🚀 Workflow</h2>
  <p>
    The API integrates seamlessly with the EA ecosystem:
  </p>
  <ul>
    <li>A user authenticates through the <strong>EA-IdentityService</strong> API.</li>
    <li>Upon successful authentication, a message is sent via an RPC queue to this <strong>Customers API</strong>.</li>
    <li>The API performs all necessary validations regarding the customer.</li>
    <li>If validations pass, the API responds to <strong>EA-IdentityService</strong>, confirming that the customer was created successfully, allowing the authentication process to continue.</li>
    <li>If customer creation fails, the API responds with a failure message. As a result, <strong>EA-IdentityService</strong> rolls back the user authentication and returns an error.</li>
  </ul>

  <h2>🛠️ Technologies Used</h2>
  <ul>
    <li><strong>.NET</strong> framework</li>
    <li><strong>RabbitMQ</strong> and <strong>EasyNetQ</strong> for messaging</li>
    <li><strong>SQL Server</strong> as the database</li>
    <li><strong>Dapper</strong> for lightweight data access</li>
    <li>Custom library: <strong>EA.CommonLib</strong></li>
  </ul>

  <h2>📚 Patterns and Best Practices</h2>
  <ul>
    <li><strong>CQRS</strong> (Command Query Responsibility Segregation)</li>
    <li><strong>Clean Architecture</strong></li>
    <li><strong>Clean Code</strong> principles</li>
  </ul>

  <h2>🐋 Requirements</h2>
  <ul>
    <li><code>RabbitMQ</code> installed and configured</li>
    <li><code>SQL Server</code> running</li>
    <li><code>.NET SDK</code> version 6.0 or higher</li>
    <li>Custom library: <strong>EA.CommonLib</strong> properly integrated</li>
  </ul>

  <h2>⚙️ How to Run</h2>
  <ol>
    <li>Clone this repository: <code>git clone https://github.com/your-repository.git</code></li>
    <li>Restore dependencies: <code>dotnet restore</code></li>
    <li>Update the <code>appsettings.json</code> file with the appropriate configuration for RabbitMQ and SQL Server.</li>
    <li>Run the application: <code>dotnet run</code></li>
  </ol>

  <h2>✅ Tests</h2>
  <p>
    To execute the tests, use the command: <code>dotnet test</code>.
  </p>
</div>

<footer>
  Made with ❤️ by Guilherme
</footer>
