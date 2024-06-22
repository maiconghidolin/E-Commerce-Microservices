import { Module } from "@nestjs/common";
import { UserModule } from "./user/user.module";
import { CassandraStartup } from "./cassandra/cassandra.startup";
import { AuthModule } from "./auth/auth.module";
import { APP_GUARD } from "@nestjs/core";
import { RolesGuard } from "./auth/roles.guard";

@Module({
  imports: [UserModule, AuthModule],
  controllers: [],
  providers: [CassandraStartup],
})
export class AppModule {}
