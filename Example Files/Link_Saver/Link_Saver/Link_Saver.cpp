#include "pch.h"
#include <mysql.h>
#include <iostream>
#include <string>

int main()
{
	int qstate;

	//MySQL Verbindung
	MYSQL* connection;
	MYSQL_ROW row;
	MYSQL_RES *res;
	connection = mysql_init(0);

	connection = mysql_real_connect(connection, "localhost", "root", "Ixcb3EM^c*Mck75^", "linksaver", 3306, NULL, 0);

	if (connection) {
		puts("Successful connection!");

		std::string query = "SELECT * FROM Links";
		const char* q = query.c_str();
		qstate = mysql_query(connection, q);
		if (!qstate)
		{
			res = mysql_store_result(connection);
			while (row = mysql_fetch_row(res))
			{
				printf("ID: %s, Link %s, Title %s, Time %s, Category %s\n", row[0], row[1], row[2], row[3], row[4]);
			}
		}
		else
		{
			std::cout << "Query failed: " << mysql_error(connection) << std::endl;
		}
	}
	else {
		puts("Connection failed!");
	}
	while (true)
	{
		//wait for command input
		std::string Command;
		std::cout << "\nCommands: new | list | delete" << std::endl;
		std::cin >> Command;

		//Create new link
		if (Command == "new") {
			std::string preText;
			std::string link;
			std::string title;
			std::string time;
			std::string category;

			std::cout << "Paste a new link in here: " << std::endl;
			std::cin >> link;
			std::cout << "\nHow do you wanna call the link?: " << std::endl;
			std::cin >> title;
			std::cout << "\nPress 'Enter' if you don't have a specific time." << std::endl;
			std::cin >> time;
			std::cout << "\nWhat category should be the link in?: " << std::endl;
			std::cin >> category;

			//Put data into database
			std::string query = "INSERT into Links (Link, Title, Time, Category) values('" + link + "','" + title + "','" + time + "','" + category + "')";
			const char* q = query.c_str();
			qstate = mysql_query(connection, q);
			std::cout << "\n\n\n\n\n\n\n\n\n\n\n\n" + query;

		}
		//List all links
		else if (Command == "list") {
			std::string query = "SELECT * FROM Links";
			const char* q = query.c_str();
			qstate = mysql_query(connection, q);
			if (!qstate)
			{
				std::cout << "\n\n\n\n\n\n\n\n\n\n\n\n";
				res = mysql_store_result(connection);
				while (row = mysql_fetch_row(res))
				{
					printf("ID: %s, Link %s, Title %s, Time %s, Category %s\n", row[0], row[1], row[2], row[3], row[4]);
				}
			}
		}
		else if (Command == "delete") {
			std::string id;
			std::cout << "Type the id number in here: " << std::endl;
			std::cin >> id;
			std::string query = "DELETE FROM Links where id=" + id;
			const char* q = query.c_str();
			qstate = mysql_query(connection, q);
			std::cout << "\n\n\n\n\n\n\n\n\n\n\n\n" + query;
		}
	}
}
