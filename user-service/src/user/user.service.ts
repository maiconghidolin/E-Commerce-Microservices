import { Injectable, OnModuleInit } from "@nestjs/common";
import { mapping } from "cassandra-driver";
import { CreateUserDto } from "./dto/create-user.dto";
import { UpdateUserDto } from "./dto/update-user.dto";
import { CassandraService } from "src/cassandra/cassandra.service";
import { User } from "./entities/user.entity";
import { UserProducerService } from "src/queues/user-exchange.producer";

@Injectable()
export class UserService implements OnModuleInit {
  constructor(
    private cassandraService: CassandraService,
    private userProducerService: UserProducerService,
  ) {}

  userMapper: mapping.ModelMapper<User>;

  onModuleInit() {
    console.log("UserService initialized");

    const mappingOptions: mapping.MappingOptions = {
      models: {
        User: {
          tables: ["user"],
          mappings: new mapping.UnderscoreCqlToCamelCaseMappings(),
        },
      },
    };

    this.userMapper = this.cassandraService
      .createMapper(mappingOptions)
      .forModel("User");
  }

  async create(createUserDto: CreateUserDto) {
    let user = {
      id: crypto.randomUUID(),
      name: createUserDto.name,
      email: createUserDto.email,
      password: createUserDto.password,
      role: createUserDto.role,
      number: createUserDto.number,
      deviceId: createUserDto.deviceId,
      created_at: new Date(),
      updated_at: new Date(),
      deleted: false,
    };

    await this.userMapper.insert(user);

    console.log("User created");

    await this.userProducerService.publish("user.created", user);

    console.log("Notification sended");
  }

  async findAll() {
    const query =
      "SELECT * FROM userdb.user WHERE deleted=false ALLOW FILTERING";

    const results = await this.userMapper.mapWithQuery(query, () => [])([]);
    return results.toArray();
  }

  async findOne(id: string) {
    return await this.userMapper.get({ id });
  }

  async emailAlreadyExists(email: string): Promise<boolean> {
    const query =
      "SELECT * FROM userdb.user WHERE email = :email ALLOW FILTERING";

    const results = await this.userMapper.mapWithQuery(query, (params) => [
      params.email,
    ])({ email: email });

    return results.toArray().length > 0;
  }

  async getByEmailAndPassword(email: string, password: string): Promise<User> {
    const query =
      "SELECT * FROM userdb.user WHERE email = :email AND password = :password ALLOW FILTERING";

    const results = await this.userMapper.mapWithQuery(query, (params) => [
      params.email,
      params.password,
    ])({ email: email, password: password });

    return results.first();
  }

  async update(id: string, updateUserDto: UpdateUserDto) {
    const user = await this.findOne(id);

    await this.userMapper.update({
      id,
      name: updateUserDto.name ?? user.name,
      email: updateUserDto.email ?? user.email,
      password: updateUserDto.password ?? user.password,
      role: updateUserDto.role ?? user.role,
      number: updateUserDto.number ?? user.number,
      deviceId: updateUserDto.deviceId ?? user.deviceId,
      updated_at: new Date(),
    });

    console.log("User updated");
  }

  async remove(id: string) {
    await this.userMapper.update({
      id,
      deleted_at: new Date(),
      deleted: true,
    });
  }
}
