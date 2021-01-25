﻿// <auto-generated />
using System;
using BrgyProfileCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BrgyProfileCore.Migrations
{
    [DbContext(typeof(BrgyContext))]
    [Migration("20210125142454_AdditionalResidentFields_2")]
    partial class AdditionalResidentFields_2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("BrgyProfileCore.Household", b =>
                {
                    b.Property<int>("HouseholdId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("HeadResidentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("HouseholdName")
                        .HasColumnType("TEXT");

                    b.HasKey("HouseholdId");

                    b.ToTable("Households");
                });

            modelBuilder.Entity("BrgyProfileCore.Resident", b =>
                {
                    b.Property<int>("ResidentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("ContactNumber")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Disability")
                        .HasColumnType("TEXT");

                    b.Property<string>("DistanceofWaterSourcefromHouse")
                        .HasColumnType("TEXT");

                    b.Property<int>("FamilyNo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Four_Ps")
                        .HasColumnType("TEXT");

                    b.Property<string>("Gender")
                        .HasColumnType("TEXT");

                    b.Property<string>("GeohazardLocation")
                        .HasColumnType("TEXT");

                    b.Property<string>("Grade_YearLevelofSchoolAttendance")
                        .HasColumnType("TEXT");

                    b.Property<string>("HighestEducationalAttainment")
                        .HasColumnType("TEXT");

                    b.Property<string>("HouseOwnership")
                        .HasColumnType("TEXT");

                    b.Property<int?>("HouseholdId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("IndigenousPeopleMembership")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<int>("LineNo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LotOwnershipwhereHouseisLocated")
                        .HasColumnType("TEXT");

                    b.Property<string>("MajorOccupationofEarningHHMember")
                        .HasColumnType("TEXT");

                    b.Property<string>("MaritalStatus")
                        .HasColumnType("TEXT");

                    b.Property<string>("MiddleName")
                        .HasColumnType("TEXT");

                    b.Property<string>("NHTS")
                        .HasColumnType("TEXT");

                    b.Property<string>("No_ofChildren_BornAlive")
                        .HasColumnType("TEXT");

                    b.Property<string>("No_ofChildren_StillLiving")
                        .HasColumnType("TEXT");

                    b.Property<string>("OtherSourceofIncome")
                        .HasColumnType("TEXT");

                    b.Property<string>("PHICMembershipSponsor")
                        .HasColumnType("TEXT");

                    b.Property<string>("ReasonforDroppingOutofSchool")
                        .HasColumnType("TEXT");

                    b.Property<string>("RelationshiptoHHHead")
                        .HasColumnType("TEXT");

                    b.Property<string>("Religion")
                        .HasColumnType("TEXT");

                    b.Property<int?>("SitioId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SourceofWaterforDrinking")
                        .HasColumnType("TEXT");

                    b.Property<string>("SourceofWaterforGeneralUse")
                        .HasColumnType("TEXT");

                    b.Property<string>("SpecialSkills")
                        .HasColumnType("TEXT");

                    b.Property<string>("TotalActualIncomeofEarningMember")
                        .HasColumnType("TEXT");

                    b.Property<string>("TypeofFuel_Cooking")
                        .HasColumnType("TEXT");

                    b.Property<string>("TypeofFuel_Lighting")
                        .HasColumnType("TEXT");

                    b.Property<string>("TypeofGarbageDisposal")
                        .HasColumnType("TEXT");

                    b.Property<string>("TypeofToilet")
                        .HasColumnType("TEXT");

                    b.Property<string>("WaterLevel")
                        .HasColumnType("TEXT");

                    b.HasKey("ResidentId");

                    b.HasIndex("HouseholdId");

                    b.HasIndex("SitioId");

                    b.ToTable("Residents");
                });

            modelBuilder.Entity("BrgyProfileCore.Sitio", b =>
                {
                    b.Property<int>("SitioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("SitioName")
                        .HasColumnType("TEXT");

                    b.HasKey("SitioId");

                    b.ToTable("Sitio");
                });

            modelBuilder.Entity("BrgyProfileCore.Resident", b =>
                {
                    b.HasOne("BrgyProfileCore.Household", "Household")
                        .WithMany("Residents")
                        .HasForeignKey("HouseholdId");

                    b.HasOne("BrgyProfileCore.Sitio", "Sitio")
                        .WithMany("Residents")
                        .HasForeignKey("SitioId");

                    b.Navigation("Household");

                    b.Navigation("Sitio");
                });

            modelBuilder.Entity("BrgyProfileCore.Household", b =>
                {
                    b.Navigation("Residents");
                });

            modelBuilder.Entity("BrgyProfileCore.Sitio", b =>
                {
                    b.Navigation("Residents");
                });
#pragma warning restore 612, 618
        }
    }
}
