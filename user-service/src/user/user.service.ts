import { Injectable, OnModuleInit } from "@nestjs/common";
import { mapping } from "cassandra-driver";
import { CreateUserDto } from "./dto/create-user.dto";
import { UpdateUserDto } from "./dto/update-user.dto";
import { CassandraService } from "src/cassandra/cassandra.service";
import { User } from "./entities/user.entity";

@Injectable()
export class UserService implements OnModuleInit {
  constructor(private cassandraService: CassandraService) {}

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
    // buscar por email

    await this.userMapper.insert({
      id: crypto.randomUUID(),
      name: createUserDto.name,
      email: createUserDto.email,
      password: createUserDto.password,
      role: createUserDto.role,
      created_at: new Date(),
      updated_at: new Date(),
    });

    console.log("User created");
  }

  async findAll() {
    const query =
      "SELECT * FROM userdb.user WHERE deleted_at='NULL' ALLOW FILTERING";

    const results = await this.userMapper.mapWithQuery(query, () => [])([]);
    return results.toArray();
  }

  async findOne(id: string) {
    return await this.userMapper.get({ id });
  }

  async update(id: string, updateUserDto: UpdateUserDto) {
    const user = await this.findOne(id);

    await this.userMapper.update({
      id,
      name: updateUserDto.name ?? user.name,
      email: updateUserDto.email ?? user.email,
      password: updateUserDto.password ?? user.password,
      role: updateUserDto.role ?? user.role,
      updated_at: new Date(),
    });

    console.log("User updated");
  }

  async remove(id: string) {
    await this.userMapper.update({
      id,
      deleted_at: new Date(),
    });
  }
}
