import { Module } from "@nestjs/common";
import { UserService } from "./user.service";
import { UserController } from "./user.controller";
import { CassandraService } from "src/cassandra/cassandra.service";

@Module({
  controllers: [UserController],
  providers: [UserService, CassandraService],
})
export class UserModule {}
