using System;
using System.Collections.Generic;	
using System.Linq;
namespace Calculator
{
    public class Program
    {
        public static string whitespace(int amount, int take){
            string white = " ";
            white+= new string(' ', amount-take);
            return white;
        }
        static double[] calculate(double P,double I,double V,double R){
            double[] vals = {P, I, V, R};
            double[] currVals = new double[4];
            for(int i=0; i<vals.Length; i++){
                if(vals[i] !=0){
                    currVals[i] = 1;     
                }
            }

            //List out all different possibilities
            if(currVals[0] != 0 && currVals[1] != 0){ // P, I = V
                vals[2] = P/I;
            }
            else if(currVals[0] != 0 && currVals[2] != 0){ // P, V = I
                vals[1] = P/V;
            }
            else if(currVals[0] != 0 && currVals[3] != 0){ // P, R 
                vals[2] = Math.Sqrt(P/R);
            }
            else if(currVals[1] != 0 && currVals[2] != 0){ // I, V = P                  NOT SOLVING FOR RRRRR
                vals[0] = I*V;
                vals[3] = V/I;
            }
            else if(currVals[1] != 0 && currVals[3] != 0){ // I, R = V
                vals[2] = I*R;
            }
            else if(currVals[2] != 0 && currVals[3] != 0){ // V, R = I
                vals[1] = V/R;
            }
            if(vals[0] == 0){
                vals[0] = vals[1] * vals[2];
            }
            else if(vals[1] == 0){
                vals[1] = vals[0] / vals[2];
            }
            else if(vals[2] == 0){
                vals[2] = vals[1] * vals[3];
            }
            else if(vals[3] == 0){
                vals[3] = vals[2] / vals[1];
            }
            return vals;
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
            while(counter !=2){
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
                        if(counter >2){
                            break;
                        }
                        else{
                            Console.WriteLine("Error, 2 values are required.");
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
            int length = default(int);
            for(int i=0; i<results.Length; i++){
                if(length < (results[i].ToString()).Length){
                    length = (results[i].ToString()).Length;
                }          
            }
            string barrier = new string('=', length+8);
            string[] lengths = new string[4];
            for(int i=0; i<4; i++){
                int vars = Convert.ToString(results[i]).Length;
                lengths[i] = (whitespace(length, vars) + "||");
            }
            Console.WriteLine("//" + barrier + "\\");
            Console.WriteLine($"|| P -- {results[0]}" + lengths[0]);
            Console.WriteLine($"|| I -- {results[1]}" + lengths[1]);
            Console.WriteLine($"|| V -- {results[2]}" + lengths[2]);
            Console.WriteLine($"|| R -- {results[3]}" + lengths[3]);
            Console.WriteLine("\\" + barrier + "//");
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