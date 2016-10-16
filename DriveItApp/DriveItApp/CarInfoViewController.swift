//
//  CarInfoViewController.swift
//  DriveItApp
//
//  Created by Shaheen Sharifian on 10/16/16.
//  Copyright © 2016 Shaheen Sharifian. All rights reserved.
//

import UIKit

class CarInfoViewController: UIViewController, UITextFieldDelegate {

    @IBOutlet weak var makeLabel: UITextField!
    @IBOutlet weak var modelLabel: UITextField!
    
    @IBOutlet weak var hybridLable: UITextField!
    @IBOutlet weak var yearLabel: UITextField!
    let pIdDictionary = ["04", "05", "0A", "0C", "0D", "1F", "2F", "52", "5B", "5C", "5E", "61"]

    let webApiUrl = "http://driveitwebapp.azurewebsites.net/api/CarValues"

    override func viewDidLoad() {
        super.viewDidLoad()
        makeLabel.delegate = self
        modelLabel.delegate = self
        hybridLable.delegate = self
        yearLabel.delegate = self
        // Do any additional setup after loading the view.
    }

    @IBAction func postCarInformation(_ sender: AnyObject) {
        postCarInfo()
        for i in 0 ..< 20 {
            postCarInformationFakeData()
        }
        postCarInformationFakeData(pId: "21")
        UserDefaults.standard.setValue("HarambeIsAlive", forKey: "Harambe")
    }
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    func postCarInformationFakeData() {
        let pId = pIdDictionary[Int(arc4random_uniform(UInt32(pIdDictionary.count) - 1))]
        postCarInformationFakeData(pId: pId)
    }
    
    func postCarInformationFakeData(pId: String) {
        let randomCarId = Int(arc4random_uniform(6) + 3)
        let randomValue = Double(arc4random_uniform(100))
        
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

    
    func postCarInfo() {
        let paramString = "make=\(self.makeLabel.text!)&model=\(modelLabel.text!)&year=\(yearLabel.text!)&isHybrid=\(hybridLable.text!)"
        
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
                print("HARAMBE")
            }
            
            let responseString = String(data: data, encoding: .utf8)
            print("responseString = \(responseString)")
            UserDefaults.standard.setValue(Int(responseString!)!, forKey: "carId")
        }
        task.resume()
    }
    
    func textFieldShouldReturn(_ textField: UITextField) -> Bool {
        textField.resignFirstResponder()
        return true
    }
    
    
    /**
     * Called when the user click on the view (outside the UITextField).
     */
    
}
