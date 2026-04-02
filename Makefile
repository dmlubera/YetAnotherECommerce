DEV_COMPOSE_FILE=docker-compose-dev.yml

.PHONY: compose-up-dev
compose-up-dev:
	docker compose -f $(DEV_COMPOSE_FILE) up --build --no-deps azurite database frontend servicebus-emulator mssql mailpit

.PHONY: compose-up-build
compose-up-build:
	docker compose -f $(DEV_COMPOSE_FILE) up --build

.PHONY: compose-down
compose-down:
	docker compose -f $(DEV_COMPOSE_FILE) down