import { HelloFromFile1 } from './file2';
import { file3Function } from './file3'; 

function GreetFromTS() {
    alert('hello from ts');
    HelloFromFile1();
    file3Function();
}

class Test {
    sayHello() {
        alert('hello');
    }
}