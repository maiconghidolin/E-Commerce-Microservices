import { Injectable } from "@nestjs/common";
import { Client, mapping, auth } from "cassandra-driver";

@Injectable()
export class CassandraService {
  client: Client;
  mapper: mapping.Mapper;

  private createClient() {
    this.client = new Client({
      contactPoints: ["127.0.0.1"],
      keyspace: "userdb",
      localDataCenter: "datacenter1",
      authProvider: new auth.PlainTextAuthProvider("admin", "admin"),
      protocolOptions: { port: 9042 },
    });
  }

  createMapper(mappingOptions: mapping.MappingOptions) {
    if (this.client == undefined) {
      this.createClient();
    }
    return new mapping.Mapper(this.client, mappingOptions);
  }
}
