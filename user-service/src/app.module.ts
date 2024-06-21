import { Module } from "@nestjs/common";
import { UserModule } from "./user/user.module";
import { CassandraStartup } from "./cassandra/cassandra.startup";

@Module({
  imports: [UserModule],
  controllers: [],
  providers: [CassandraStartup],
})
export class AppModule {}
