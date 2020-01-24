#include "pch.h"
#include <iostream>

int main()
{
	while (true)
	{
		//Variablen
		int Fahrenheit;
		int Celsius;
		int CelsiusFahrenheit_Abfrage;

		//Temperatur Abfrage
		std::cout << "Gib eine auswahl ein: \n Celsius \t 1 \n Fahrenheit \t 2 \n\n";
		std::cin >> CelsiusFahrenheit_Abfrage;

		if (CelsiusFahrenheit_Abfrage == 1) {
			//Celsius Abfrage
			std::cout << "Gib hier die Celsius Zahl ein: ";
			std::cin >> Celsius;

			//Berechnung Celsius zu Fahrenheit
			Fahrenheit = (Celsius * 9 / 5) + 32;

			//Ausgabe in Fahrenheit und Celsius
			std::cout << "Fahrenheit: " << Fahrenheit << "\n" << "Celsius: " << Celsius << "\n\n";
		}
		else {
			//Fahrenheit Abfrage
			std::cout << "Gib hier die fahrenheit Zahl ein: ";
			std::cin >> Fahrenheit;

			//Berechnung Celsius zu Fahrenheit
			Celsius = (Fahrenheit - 32) * 5 / 9;

			//Ausgabe in Fahrenheit und Celsius
			std::cout << "Celsius: " << Celsius << "\n" << "Fahrenheit: " << Fahrenheit << "\n\n";
		}
	}
}

