import { Module } from "@nestjs/common";
import { UserService } from "./user.service";
import { UserController } from "./user.controller";
import { CassandraService } from "src/cassandra/cassandra.service";
import { QueueModule } from "src/queues/queue.module";

@Module({
  imports: [QueueModule],
  controllers: [UserController],
  providers: [UserService, CassandraService],
  exports: [UserService],
})
export class UserModule {}
