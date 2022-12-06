interface ICalculatingFunction
{
    void calculatedmethod(double weight, double pmax, double hmax);
    string res();
}
abstract class Repair
{
    protected double[] compression = new double[4];
    protected double mincomp, maxcomp, aligment = 0.0;
    protected byte overrollcomp = 0;
    protected string fuel = "";

    public Repair()
    {

    }
    public Repair(double[] compression, string fuel)
    {
        this.compression = compression;
        this.fuel = fuel;
    }
    public Repair(double[] compression)
    { this.compression = compression; }
    public double mincomp_
    {
        get { return mincomp; }
        set
        {
            if (value != double.NaN)
            { mincomp = value; }
        }
    }
    public double maxcomp_
    {
        get { return maxcomp; }
        set
        {
            if (value != double.NaN)
            { maxcomp = value; }
        }
    }
    public double aligment_
    {
        get { return aligment; }
    }
    public abstract double compressiondiagostic();
    
    public void showdiagnostic()
    {
        compressiondiagostic();
        Console.WriteLine(Result());
    }
    public virtual string Result()
    {
        return $"";
    }
    ~Repair()
    {
        Console.WriteLine("Объект уничтожен");
    }
}
class Enginerepair : Repair, ICalculatingFunction
{
    private readonly static byte basekilom = 100;
    protected double avgcompsumption;
    public Enginerepair()
    {

    }
    public Enginerepair(double[] compression, string fuel)
    {
        this.compression = compression;
        this.fuel = fuel;
    }
    public Enginerepair(double avgcompsumption)
    {
        this.avgcompsumption = avgcompsumption;
    }
    public static Enginerepair operator /(Enginerepair p, Enginerepair p2)
    {
        Enginerepair p3 = new Enginerepair();
        if (p2.avgcompsumption == 0)
        {
            throw new DivideByZeroException();
        }
        else
        {
            p3.avgcompsumption = p.avgcompsumption / p2.avgcompsumption * basekilom;
            return p3;
        }
    }
    public string res()
    {
        return String.Format($"Количество киллометров при указаных литрах и расходе: {avgcompsumption}");
    }
    public void calculatedmethod(double weight, double pmax, double hmax)
    {
        if (weight > 0 && pmax > 0 && hmax > 0)
        {
            double avg = weight * pmax * hmax;
            Console.WriteLine($"Расчет давления турбины: {avg} мм^3");
        }
        else
        {
            Console.WriteLine("Указаны некорректные данные!");
        }
    }
    public override double compressiondiagostic()
    {
        byte count = 0;
        if (fuel == "Дизель")
        {
            mincomp_ = 22.0;
            maxcomp_ = 32.0;
            foreach (int i in compression)
            {
                if (i < 0)
                {
                    overrollcomp = 2;
                }
                else if (mincomp_ < i && i < maxcomp_)
                {
                    count++;
                    if (count == 4)
                    {
                        overrollcomp = 1;
                    }
                }
            }
        }
        else if (fuel == "Бензин")
        {
            mincomp_ = 12.0;
            maxcomp_ = 14.0;
            foreach (int i in compression)
            {
                if (i < 0)
                {
                    overrollcomp = 2;
                }
                if (mincomp_ < i && i < maxcomp_)
                {
                    count++;
                    if (count == 4)
                    {
                        overrollcomp = 1;
                    }
                }
            }
        }
        return overrollcomp;
    }
    public override string Result()
    {
        if (overrollcomp == 1)
        {
            return $"Компрессия в норме \nТип топлива: {fuel} \nКомпрессия в цилиндре 1 - {compression[0]} \nКомпрессия в цилиндре 2 - {compression[1]} " +
                $"\nКомпрессия в цилиндре 3 - {compression[2]} \nКомпрессия в цилиндре 4 - {compression[3]}";

        }
        else if (overrollcomp == 0)
        {
            return $"Компрессия нарушена  \nТип топлива: {fuel} \nКомпрессия в цилиндре 1 - {compression[0]} \nКомпрессия в цилиндре 2 - {compression[1]} " +
                $"\nКомпрессия в цилиндре 3 - {compression[2]} \nКомпрессия в цилиндре 4 - {compression[3]}";
        }
        else
        {
            return $"Не корректные данные!";
        }
        

    }
    ~Enginerepair()
    {
        Console.WriteLine("Объект уничтожен");
    }
}
class Suspension : Repair, ICalculatingFunction
{
    private double clirens, leftwheels, rightwheels;
    public Suspension()
    {

    }
    public Suspension(double clirens)
    {
        this.clirens = clirens;
    }
    public Suspension(double leftwheels, double rightwheels)
    {
        this.leftwheels = leftwheels;
        this.rightwheels = rightwheels;
    }
    public Suspension(double[] compression)
    {
        this.compression = compression;
    }
    public override double compressiondiagostic()
    {
        mincomp_ = 0.4;
        maxcomp_ = -0.4;
        byte count = 0;
        foreach (int i in compression)
        {
            if ( i == aligment_ || mincomp_ < i && i < maxcomp_)
            {
                count++;
                if (count == 4)
                {
                    overrollcomp = 1;
                }
            }
        }
        return overrollcomp;
    }
    public static bool operator ==(Suspension s1, Suspension s2)
    {
        if (s1.clirens == s2.clirens)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool operator !=(Suspension s1, Suspension s2)
    {
        if (s1.clirens != s2.clirens)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public override string Result()
    {
        if (overrollcomp == 1)
        {
            return $"Сход-развал установлен правильно \nПЛ колесо - {compression[0]} \nПП колесо - {compression[1]} " +
                $"\nЗЛ колесо - {compression[2]} \nЗП колесо - {compression[3]}";
        }
        else if (overrollcomp == 0)
        {
            return $"Сход-развал нарушен \nПЛ колесо - {compression[0]} \nПП колесо - {compression[1]} " +
                $"\nЗЛ колесо - {compression[2]} \nЗП колесо - {compression[3]}";
        }
        else
        {
            return $"Ошибка";
        }
    }
    public void calculatedmethod(double weight, double pmax, double hmax)
    {
        if(weight > 0 && pmax > 0 && hmax > 0)
        {
            double avg = weight / 4 * pmax / hmax;
            Console.WriteLine($"Давление в шине должно быть: {avg} psi");
        }
        else
        {
            Console.WriteLine("Указаны некорректные данные!");
        }
    }
    public static Suspension operator -(Suspension obj1, Suspension obj2)
    {
        Suspension obj3 = new Suspension();
        obj3.leftwheels = obj1.leftwheels - obj2.leftwheels;
        obj3.rightwheels = obj1.rightwheels - obj2.rightwheels;
        return obj3;
    }
    public string res()
    {
        if(rightwheels > 0 || leftwheels < 0)
        {
            return String.Format($"Автомабиль накринён в левую сторону");
        }
        else if (leftwheels > 0 || rightwheels < 0)
        {
            return String.Format($"Автомабиль накринён в правую сторону");
        }
        else
        {
            return String.Format($"Автомабиль находиться в ровном положении");
        }    
    }
    ~Suspension()
    {
        Console.WriteLine("Объект уничтожен");
    }
}
class Shortinfo : Enginerepair
{
    protected string bodytype, brand, model;
    public Shortinfo()
    {
        bodytype = "Купе";
        brand = "Nissan";
        model = "Silvia S13";
        fuel = "Бензин";
    }
    public new string res()
    {
        foreach(char i in bodytype)
        {
            foreach(char j in fuel)
            {
                if (i == '0' || i == '1' || i == '2' || i == '3' || i == '4' || i == '5' || i == '6' || i == '7' || i == '8' || i == '9')
                {
                    return String.Format("Кузов не может содержать число!");
                }
                if (j == '0' || j == '1' || j == '2' || j == '3' || j == '4' || j == '5' || j == '6' || j == '7' || i == '8' || j == '9')
                {
                    return String.Format("Тип топлива должен быть: Бензин или Дизель!");
                }
            }
            
        }
        return String.Format($"Тип кузова = {bodytype} \nМарка = {brand} \nМодель = {model} \nТип топлива = {fuel}");
    }
}
class Engineinfo : Shortinfo
{
    protected short rpm;
    protected double enginevolume, torque, hoursepower;
    protected string enginemodel;
    public Engineinfo()
    {

    }
    public Engineinfo(double hoursepower, double torque, short rpm)
    {
        enginevolume = 2.0;
        enginemodel = "SR20DET";
        this.hoursepower = hoursepower;
        this.torque = torque;
        this.rpm = rpm;
    }
    private double calulateHP()
    {
        if (hoursepower == 0)
        {
            hoursepower = (short)(rpm * torque / 9550);
            return hoursepower;
        }
        else
        {
            return hoursepower;
        }
    }
    private double calulateTorque()
    {
        if (torque == 0)
        {
            torque = hoursepower * 9550 / rpm;
            return torque;
        }
        else
        {
            return torque;
        }
    }
    public new string res()
    {
        calulateHP();
        calulateTorque();
        foreach (char i in bodytype)
        {
            foreach (char j in fuel)
            {
                if (i == '0' || i == '1' || i == '2' || i == '3' || i == '4' || i == '5' || i == '6' || i == '7' || i == '8' || i == '9')
                {
                    return String.Format("Кузов не может содержать число!");
                }
                if (j == '0' || j == '1' || j == '2' || j == '3' || j == '4' || j == '5' || j == '6' || j == '7' || i == '8' || j == '9')
                {
                    return String.Format("Тип топлива должен быть: Бензин или Дизель!");
                }
            }
        }
        return String.Format($"Тип кузова = {bodytype} \nМарка = {brand} \nМодель = {model} \nТип топлива = {fuel} " +
            $"\nМодель двигателя = {enginemodel} \nОбъём двигателя = {enginevolume} \nМощьность двигателя = {hoursepower} \nКрутящий момент = {torque}");
    }
}
class Program
{
    static void Main(string[] args)
    {
        //while (true)
        //{
        //    Enginerepair enginerepair = new Enginerepair();
        //}
        Console.ReadKey();
        Engineinfo enn = new Engineinfo(250, 477, 5000);
        Console.WriteLine(enn.res());
        Console.ReadKey();
        Console.Clear();

        double[] compression1 = { 0.0, 13.0, 13.0, 13.0 };
        Enginerepair rep = new Enginerepair(compression1, "Бензин");
        rep.showdiagnostic();
        Console.ReadKey();
        Console.Clear();

        double[] compression2 = { 13.1, 13.0, 13.0, 13.0 };
        Enginerepair hy = new Enginerepair(compression2, "Бензин");
        hy.showdiagnostic();
        Console.ReadKey();
        Console.Clear();

        double[] compression3 = { 0.0, -13.0, 13.0, 13.0 };
        Enginerepair ty = new Enginerepair(compression3, "Бензин");
        ty.showdiagnostic();
        Console.ReadKey();
        Console.Clear();

        double[] compression4 = { 25.0, 24.0, 30.0, 13.0 };
        Enginerepair rept = new Enginerepair(compression4, "Дизель");
        rept.showdiagnostic();
        Console.ReadKey();
        Console.Clear();

        double[] compression5 = { 25.1, 26.0, 24.0, 25.0 };
        Enginerepair hyh = new Enginerepair(compression5, "Дизель");
        hyh.showdiagnostic();
        Console.ReadKey();
        Console.Clear();

        double[] compression6 = { 0.0, -13.0, 13.0, 13.0 };
        Enginerepair tyt = new Enginerepair(compression6, "Дизель");
        tyt.showdiagnostic();
        Console.ReadKey();
        Console.Clear();

        double[] compression7 = { 0.0, 0.12, -0.3, 0.1 };
        Suspension s = new Suspension(compression7);
        s.showdiagnostic();
        Console.ReadKey();
        Console.Clear();

        double[] compression8 = { 0.2, 2.1, 0, 0 };
        Suspension sp = new Suspension(compression8);
        sp.showdiagnostic();
        Console.ReadKey();
        Console.Clear();

        Enginerepair par = new Enginerepair(40);
        Enginerepair par2 = new Enginerepair(6.5);
        Enginerepair par3 = par / par2;
        Console.WriteLine(par3.res());
        Console.ReadKey();
        Console.Clear();

        Suspension sus1 = new Suspension(135);
        Suspension sus2 = new Suspension(136);
        if (sus1 == sus2)
        {
            Console.WriteLine("Клиренс автомобиля соответствует стандарту");
        }
        else
        {
            Console.WriteLine("Клиренс не соответсвует стандарту!");
        }
        Console.ReadKey();
        Console.Clear();

        Suspension sus3 = new Suspension(135);
        Suspension sus4 = new Suspension(135);
        if (sus3 == sus4)
        {
            Console.WriteLine("Клиренс автомобиля соответствует стандарту");
        }
        else
        {
            Console.WriteLine("Клиренс не соответсвует стандарту!");
        }
        Console.ReadKey();
        Console.Clear();

        Shortinfo sh = new Shortinfo();
        Console.WriteLine(sh.res());
        Console.ReadKey();
        Console.Clear();

        Engineinfo en = new Engineinfo(0, 1000, 5000);
        Console.WriteLine(en.res());
        Console.ReadKey();
        Console.Clear();

        en.calculatedmethod(500, 0.85, 2.7);
        Console.ReadKey();
        Console.Clear();

        en.calculatedmethod(500, -0.85, 2.7);
        Console.ReadKey();
        Console.Clear();

        s.calculatedmethod(1200, 50, 615);
        Console.ReadKey();
        Console.Clear();

        s.calculatedmethod(1200, -50, 615);
        Console.ReadKey();
        Console.Clear();

        Suspension tran1 = new Suspension(23, 15);
        Suspension tran2 = new Suspension(24, 15);
        Suspension tran3 = tran1 - tran2;
        Console.WriteLine(tran3.res());
        Console.WriteLine();
        Console.WriteLine();
        Suspension tran4 = new Suspension(23, 15);
        Suspension tran5 = new Suspension(22, 15);
        Suspension tran6 = tran4 - tran5;
        Console.WriteLine(tran6.res());
        Console.WriteLine();
        Console.WriteLine();
        Suspension tran7 = new Suspension(24, 15);
        Suspension tran8 = new Suspension(24, 15);
        Suspension tran9 = tran7 - tran8;
        Console.WriteLine(tran9.res());
        Console.ReadKey();
    }
}