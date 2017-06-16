# ThreadAndNetwork
Разработка клиент-серверного приложения, реализую многопоточный клиент и сервер разными способами.
Каждая ветвь соответсвует разичным технология создания потоков и сетевого взаимодействия.
Ветви храняться для примеров.
Текущая ветвь, последний коммит ClientServer_AbstractClass(15.06) - клиент и сервер представлены в виде абстрактных классов
хранящих в библиотеке классов, данные для обмена храняться в библиотеке классов, многопоточный сервер и клиент реализованы через
пул потоков, передача данных осуществляется по протоколу TCP с сериализацией/десериализацией в бинарный формат.

Making Client-Server console app, using different technologys for multithreading and networking
Every branch shows different technologys, saving for an example
Current branch - **ClientServer_AbstractClass (15.06)** - client and server is an extension of abstract classes, 
saving in lib. Format of exchanging info saving in lib too. Multithreading of client and server make throu ThreadPool. Exchenging throu TCP, using serialization/deserialization in binary format.

- [] Use JSON instead of binary 
- [] Use UDP instead of TCP
