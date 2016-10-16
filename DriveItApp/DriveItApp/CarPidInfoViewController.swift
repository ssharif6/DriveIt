//
//  CarPidInfoViewController.swift
//  DriveItApp
//
//  Created by Shaheen Sharifian on 10/16/16.
//  Copyright © 2016 Shaheen Sharifian. All rights reserved.
//

import UIKit

class CarPidInfoViewController: UIViewController, UITableViewDelegate, UITableViewDataSource {
    let webApiUrl = "http://driveitwebapp.azurewebsites.net/api/CarValues"
    let pIdDictionary = ["04", "05", "0A", "0C", "0D", "1F", "2F", "52", "5B", "5C", "5E", "61"]

    var userModel = [String : AnyObject]()
    var pidModel = [String : AnyObject]()
    var pidArray = [AnyObject]()
    
    @IBOutlet weak var yearLabel: UILabel!
    @IBOutlet weak var isHybridLabel: UILabel!
    @IBOutlet weak var modelLabel: UILabel!
    @IBOutlet weak var makeLabel: UILabel!
    @IBOutlet weak var titleLabel: UILabel!
    @IBOutlet weak var tableView: UITableView!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        self.tableView.delegate = self
        self.tableView.dataSource = self
        getData()

    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
        
    }
    
    func getData() {
        /*
        let getUrl = NSURL(string: "http://driveitwebapp.azurewebsites.net/api/CarValues")

        let taskGet = URLSession.shared.dataTask(with: getUrl! as URL) {(data, response, error) in
            print(NSString(data: data!, encoding: String.Encoding.utf8.rawValue))
            print(response)
            let shitString = String(data: data!, encoding: String.Encoding.utf8)
            carId = Int(shitString!)!
            
        }
        taskGet.resume()
 */
        var carId = 5


        if UserDefaults.standard.value(forKey: "cardId") != nil {
            carId = UserDefaults.standard.value(forKey: "carId") as! Int
        }
        let url = NSURL(string: "http://driveitwebapp.azurewebsites.net/api/CarValues?userId=1&carId=5")
            
            let task = URLSession.shared.dataTask(with: url! as URL) {(data, response, error) in
                print(NSString(data: data!, encoding: String.Encoding.utf8.rawValue))
                //print(response)
                let responseData = String(data: data!, encoding: String.Encoding.utf8)
                var jObject = self.convertStringToDictionary(text: responseData!)
                print(jObject)

                self.userModel = jObject?["UserModel"] as! [String : AnyObject]
                DispatchQueue.main.async(execute: { 
                    self.titleLabel.text = "\(self.userModel["FirstName"]as! String)'s Car Information)"
                    self.makeLabel.text = "Make: Ford"
                    self.modelLabel.text = "Model: CMax"
                    self.yearLabel.text = "Year: \(self.userModel["CarYear"] as! String)"
                })
                
                self.pidArray = (jObject?["PidModel"])! as! [AnyObject]
                self.tableView.reloadData()
                
            }
            task.resume()
    }
    
    func convertStringToDictionary(text: String) -> [String:AnyObject]? {
        if let data = text.data(using: String.Encoding.utf8) {
            do {
                return try JSONSerialization.jsonObject(with: data, options: []) as? [String:AnyObject]
            } catch let error as NSError {
                print(error)
            }
        }
        return nil
    }
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        
    }
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return self.pidArray.count
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        if let cell : PdiTableCell = self.tableView.dequeueReusableCell(withIdentifier: "PidCell") as? PdiTableCell {
            cell.userValueLabel.text = "\(self.pidArray[indexPath.row]["Value"] as! Double) \(self.pidArray[indexPath.row]["Units"] as! String)"
            cell.descriptionLabel.text = pidArray[indexPath.row]["Description"] as? String
            cell.nationalAvgLabel.text = "\(pidArray[indexPath.row]["NationalValue"] as! Double) \(self.pidArray[indexPath.row]["Units"] as! String)"
            return cell
        }
        return PdiTableCell()
    }
    
    func postShit() {
        for i in 0 ..< 20 {
            postCarInformationFakeData()
        }
        postCarInformationFakeData(pId: "21")
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
    @IBAction func refreshPage(_ sender: AnyObject) {
        postShit()
    }

}
