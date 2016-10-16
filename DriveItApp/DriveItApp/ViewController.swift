//
//  ViewController.swift
//  DriveItApp
//
//  Created by Shaheen Sharifian on 10/15/16.
//  Copyright © 2016 Shaheen Sharifian. All rights reserved.
//

import UIKit

class ViewController: UIViewController {
    let webApiUrl = "http://driveitwebapp.azurewebsites.net/api/CarValues"
    let pIdDictionary = ["04", "05", "0A", "0C", "0D", "1F", "21", "23", "2F", "43", "52", "5B", "5C", "5E", "61"]
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view, typically from a nib.
        //getCarInfo()
    }
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(true)
        if UserDefaults.standard.value(forKey: "Harambe") != nil {
            self.performSegue(withIdentifier: "bypassHarambe", sender: nil)
        }
    }
    
    
    func getCarPidInfo() {
        let url = NSURL(string: "http://driveitwebapp.azurewebsites.net/api/CarValues?userId=1&carId=5")
        
        let task = URLSession.shared.dataTask(with: url! as URL) {(data, response, error) in
            print(NSString(data: data!, encoding: String.Encoding.utf8.rawValue))
            print(response)
        }
        task.resume()
    }
    
    func postCarInfo() {
        let randomCarId = Int(arc4random_uniform(6) + 3)
        let randomValue = Double(arc4random_uniform(100))
        let pId = pIdDictionary[Int(arc4random_uniform(UInt32(pIdDictionary.count) - 1))]
        
        let paramString = "userId=1&carId=\(randomCarId)&value=\(randomValue)&pId=\(pId)"
        print("ParamString: " + paramString)
        var request = URLRequest(url: URL(string: webApiUrl+"?"+paramString)!)
        request.httpMethod = "POST"
        request.httpBody = paramString.data(using: .utf8)
        let task = URLSession.shared.dataTask(with: request) { data, response, error in
            guard let data = data, error == nil else {                                                                 print("error=\(error)")
                return
            }
            
            if let httpStatus = response as? HTTPURLResponse, httpStatus.statusCode != 200 {           // check for http errors
                print("statusCode should be 200, but is \(httpStatus.statusCode)")
                print("response = \(response)")
            }
            
            let responseString = String(data: data, encoding: .utf8)
            print("responseString = \(responseString)")
        }
        task.resume()
    }
    
}

