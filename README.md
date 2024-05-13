# Crystal Sharp - Saga Orchestration PostgreSQL Example
Crystal Sharp framework - Saga Orchestration code example with `PostgreSQL`.


### About This Example
This example uses `Microsoft SQL Server` for storing events and `PostgreSQL` as a saga store.


### How to Run

* Event Store database must exists in `Microsoft SQL Server`.
* Change the event store connectionstring in `appsettings.json` file.
* Saga Store database must exist in `PostgreSQL`.
* Change the saga store connectionstring in `appsettings.json` file.
* Run the `WebAPI` project.
