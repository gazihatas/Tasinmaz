export class User
{
  public userId: string="";
  public fullName:string="";
  public email:string="";
  public userName:string="";
  public role:string="";

  constructor(id:string,fullName:string, email:string,userName:string,role:string)
  {
    this.userId = id;
    this.fullName = fullName;
    this.email = email;
    this.userName = userName;
    this.role = role;
  }
}
