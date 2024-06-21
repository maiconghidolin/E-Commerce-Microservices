import { Injectable, OnApplicationBootstrap } from "@nestjs/common";
import { Client, auth } from "cassandra-driver";

@Injectable()
export class CassandraStartup implements OnApplicationBootstrap {
  client: Client;

  async onApplicationBootstrap() {
    console.log("CassandraStartup initialized");

    this._createClient();
    await this._createKeyspace();
    await this._createTable();

    this.client.shutdown();
  }

  private _createClient() {
    this.client = new Client({
      contactPoints: ["127.0.0.1"],
      localDataCenter: "datacenter1",
      authProvider: new auth.PlainTextAuthProvider("admin", "admin"),
      protocolOptions: { port: 9042 },
    });
  }

  private async _createKeyspace() {
    const query = `
      CREATE KEYSPACE IF NOT EXISTS userdb
      WITH REPLICATION = {
        'class': 'SimpleStrategy',
        'replication_factor': 1
      };
    `;

    await this.client.execute(query);

    console.log("Keyspace created");
  }

  private async _createTable() {
    const query = `
      CREATE TABLE IF NOT EXISTS userdb.user (
        id UUID PRIMARY KEY,
        name TEXT,
        email TEXT,
        password TEXT,
        role TEXT,
        created_at TIMESTAMP,
        updated_at TIMESTAMP,
        deleted_at TIMESTAMP
      );
    `;

    await this.client.execute(query);

    console.log("Table created");
  }
}
