﻿using System;
using System.Collections.Generic;	
using System.Linq;
namespace Calculator
{
    public class Program
    {
        static double[] calculate(double P = default(int), double I = default(int), double V = default(int), double R = default(int)){
            double[] results = new double[4];
            if(V == 0){
                results[2] = cvoltage(I, R);
            }
            if(I == 0){
                results[1] = ccurrent(V, R);
            }
            if(R == 0){
                results[3] = cresistance(V, I);
            }
            if(P == 0){
                if(I == 0){
                    results[1] = ccurrent(V, R);
                }
                else{
                    results[2] = cvoltage(I, R);
                }
                results[0] = cpower(I, V);
                if(R == 0){
                    results[3] = cresistance(V, I);
                }
                for(int i = 0; i<5; i++){
                Console.WriteLine(results[i]);
                }
            }
            return results;
        //     I = V/R;
        //     R = V/I;
        //     V = I*R;
        //     P = I*V;
        //     results[0] = P;
        //     results[1] = I;
        //     results[2] = V;
        //     results[3] = R;
        //     if(P == 0){
        //         Console.WriteLine("P == 0");
        //         return results;
        //     }
        //     return results;
        }
        static double cpower(double x, double y, int which = 0) {
            switch(which) {
                case 1:
                    x = x*x;
                    return x*y;
                case 2:
                    x = x*x;
                    return x/y;
                default:
                    return x*y;
            }
        }
        static double cvoltage(double i, double r) {
            return i * r;	
        }
        static double ccurrent(double v, double r) {
            return v / r;	
        }
        static double cresistance(double v, double i) {
            return v / i;	
        }
        private static void basic() {
            Console.WriteLine("Which to calculate:\n 1) Power (W)\n 2) Voltage (V)\n 3) Current (I)\n 4) Resistance (Ω)\n 5) Main Menu");
                int choice = Int32.Parse(Console.ReadLine());
                double result = 0.0;
                char unit = 'v';
                if(choice == 1) {
                    Console.WriteLine("Voltage: ");
                    double voltage = double.Parse(Console.ReadLine());
                    Console.WriteLine("Current: ");
                    double current = double.Parse(Console.ReadLine());
                    result = cpower(voltage, current);
                    unit = 'W';
                }
                else if(choice == 2){
                    Console.WriteLine("Current: ");
                    double current = double.Parse(Console.ReadLine());
                    Console.WriteLine("Resistance: ");
                    double resistance = double.Parse(Console.ReadLine());
                    result = cvoltage(current, resistance);
                    unit = 'V';
                }
                else if(choice == 3){
                    Console.WriteLine("Voltage: ");
                    double voltage = double.Parse(Console.ReadLine());
                    Console.WriteLine("Resistance: ");
                    double resistance = double.Parse(Console.ReadLine());
                    result = ccurrent(voltage, resistance);
                    unit = 'I';
                }
                else if(choice == 4) {
                    Console.WriteLine("Voltage: ");
                    double voltage = double.Parse(Console.ReadLine());
                    Console.WriteLine("Current: ");
                    double current = double.Parse(Console.ReadLine());
                    result = cresistance(voltage, current);
                    unit = 'Ω';
                }
                else if(choice == 5) {
                    menu();	
                }
                else {
                    Console.WriteLine("Error, invalid choice. Try again");
                    basic();
                    
                }	
                Console.WriteLine(String.Format("\n---------------------\n{0} = {1}\n---------------------", unit, result));
                basic();
        }
        private static void advanced() {
            var data = new Dictionary<char, double>(){
                {'P', 0},
                {'I', 0},
                {'V', 0},
                {'R', 0},
            };
            string[] vals = {"P", "I", "V", "R"};
            int counter= 0;
            while(counter !=4){
                Console.WriteLine("Please enter some data using these units: P,I,V,R (e.g. V=54.753) (to go back, type back)");
                Console.WriteLine(String.Format("P = {0}, I = {1}, V = {2}, R = {3}", data['P'], data['I'], data['V'], data['R']));
                string curr = Console.ReadLine();
                string val = "";
                try{
                    val = curr.Substring(2);
                    curr = curr.Substring(0,1);
                    if(vals.Any(curr.Contains)) {
                        try{
                            data[char.Parse(curr)] = double.Parse(val);
                            counter++;
                        }
                        catch(Exception) {
                            Console.WriteLine("Error, Invalid value given.");
                        }
                    }
                }
                catch(Exception) {
                    if(curr == ""){
                        if(counter >=2){
                            break;
                        }
                        else{
                            Console.WriteLine("Error, at least 2 values are required.");
                        }
                    }
                    else if(curr == "back"){
                        menu();
                    }
                    else{
                        Console.WriteLine("Error, Invalid value or argument");
                    }
                }
            }
            double[] results = calculate(data['P'],data['I'],data['V'],data['R']);
            for(int i=0; i<results.Length; i++){
                Console.WriteLine($"{vals[i]}, {results[i]}");
            }
        }
        private static void menu() {
            Console.WriteLine("Welcome! Please choose from the following:\n 1) Basic\n 2) Advanced");
            string choice = Console.ReadLine();
            switch(choice) {
                case "1":
                    basic();
                    break;
                case "2":
                    advanced();
                    break;
                default:
                    Console.WriteLine("Error, invalid choice. Please try again");
                    menu();
                    break;
            }
        }
        public static void Main()
        {
            menu();
        }
    }
}