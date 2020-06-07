public class RemoteControl
{
    List[] on;
    List[] off;

     public RemoteComtrol() {
        this.onComands = new Command[7];
        this.offComands = new Command[7];
        for(int i = 0; i< 7;i++){
            this.onComands[i] = new NoCommand();
            this.offComands[i] = new NoCommand();
        }
    }

    public void setCommand(int slot,Command onCommand,Command offCommand ){
        this.onComands[slot] = onCommand;
        this.offComands[slot] = offCommand;
    }

    public void onButtonWasPushed(int slot){
        this.onComands[slot].excute();
    }

    public void offButtonWasPushed(int slot){
        this.offComands[slot].excute();
    }
}

public class Light {
    public void on(){
        System.Console.WriteLine("turn on the light");
    }
    public void off(){
        System.Console.WriteLine("turn off the light");
    }
}

public class LivingRoomLight : Light {
    public override void off() {
        System.Console.WriteLine("Turn on living room light");
    }

    public override void on() {
        System.Console.WriteLine("Turn off living room light");
    }
}

public interface Command {

    void excute();
}

public class LightOnCommand : Command {

    Light light;

    public LightOnCommand(Light light) {
        this.light = light;
    }

    public void excute() {
        light.on();
    }
}

public class LigthOffCommand : Command {
    private Light light;

    public LigthOffCommand() {
    }

    public LigthOffCommand(Light light) {
        this.light = light;
    }

    public void excute() {
        light.off();
    }
}
