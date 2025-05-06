"use client";

import React, { useEffect, useState } from "react";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import * as z from "zod";
import { Button } from "@/components/ui/button";
import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";
import { trafficFlowApi, trafficLightApi } from "@/lib/api";
import { TrafficLight, TrafficDataResponse } from "@/lib/api-types";
import { PageLoading } from "@/components/layout/page-loading";
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from 'recharts';
import { AlertTriangle, Clock3 } from "lucide-react";

// Form validation schema
const formSchema = z.object({
  trafficLightId: z.string().min(1, "Traffic light is required"),
  carsNumber: z.string().min(1, "Number of cars is required"),
});

// Demo data for chart
const demoData = [
  { cars: 5, duration: 15 },
  { cars: 10, duration: 25 },
  { cars: 15, duration: 35 },
  { cars: 20, duration: 45 },
  { cars: 25, duration: 55 },
  { cars: 30, duration: 65 },
];

export default function TrafficFlowPage() {
  const [trafficLights, setTrafficLights] = useState<TrafficLight[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [isCalculating, setIsCalculating] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [result, setResult] = useState<TrafficDataResponse | null>(null);
  const [chartData, setChartData] = useState(demoData);

  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      trafficLightId: "",
      carsNumber: "",
    },
  });

  useEffect(() => {
    async function fetchTrafficLights() {
      try {
        setIsLoading(true);
        const response = await trafficLightApi.getAll();
        setTrafficLights(response.result || []);
      } catch (err) {
        console.error("Failed to fetch traffic lights:", err);
        setError("Failed to load traffic lights. Please try again.");
      } finally {
        setIsLoading(false);
      }
    }

    fetchTrafficLights();
  }, []);

  async function onSubmit(values: z.infer<typeof formSchema>) {
    try {
      setIsCalculating(true);
      setError(null);
      
      const payload = {
        trafficLightId: parseInt(values.trafficLightId),
        carsNumber: parseInt(values.carsNumber),
      };
      
      const response = await trafficFlowApi.calculateGreenLight(payload);
      
      setResult(response.result);
      
      // Add new data point to chart
      const newPoint = { 
        cars: parseInt(values.carsNumber), 
        duration: response.result.greenLightDuration
      };
      
      setChartData(prev => {
        // Keep last 9 points and add new one
        const newData = [...prev.slice(-9), newPoint];
        return newData;
      });
      
    } catch (err) {
      console.error("Failed to calculate green light duration:", err);
      setError("Failed to calculate green light duration. Please try again.");
    } finally {
      setIsCalculating(false);
    }
  }

  if (isLoading) {
    return <PageLoading />;
  }

  return (
    <div className="space-y-6">
      <h1 className="text-3xl font-bold tracking-tight">Traffic Flow Simulator</h1>
      <p className="text-muted-foreground">
        Calculate optimal green light durations based on traffic volume
      </p>
      
      <div className="grid gap-6 md:grid-cols-2">
        <Card>
          <CardHeader>
            <CardTitle>Calculate Green Light Duration</CardTitle>
            <CardDescription>
              Enter traffic data to calculate the optimal green light duration
            </CardDescription>
          </CardHeader>
          <CardContent>
            <Form {...form}>
              <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-6">
                <FormField
                  control={form.control}
                  name="trafficLightId"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Traffic Light</FormLabel>
                      <Select
                        onValueChange={field.onChange}
                        defaultValue={field.value}
                      >
                        <FormControl>
                          <SelectTrigger>
                            <SelectValue placeholder="Select a traffic light" />
                          </SelectTrigger>
                        </FormControl>
                        <SelectContent>
                          {trafficLights.map((light) => (
                            <SelectItem key={light.id} value={light.id.toString()}>
                              {light.name || `Traffic Light #${light.id}`}
                            </SelectItem>
                          ))}
                        </SelectContent>
                      </Select>
                      <FormDescription>
                        Select the traffic light to calculate timing for
                      </FormDescription>
                      <FormMessage />
                    </FormItem>
                  )}
                />
                
                <FormField
                  control={form.control}
                  name="carsNumber"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Number of Vehicles</FormLabel>
                      <FormControl>
                        <Input
                          type="number"
                          placeholder="Enter number of cars"
                          {...field}
                        />
                      </FormControl>
                      <FormDescription>
                        Enter the current number of vehicles at the intersection
                      </FormDescription>
                      <FormMessage />
                    </FormItem>
                  )}
                />
                
                <Button type="submit" disabled={isCalculating}>
                  {isCalculating ? "Calculating..." : "Calculate"}
                </Button>
                
                {error && (
                  <div className="flex items-center gap-2 text-destructive mt-4">
                    <AlertTriangle className="h-4 w-4" />
                    <p>{error}</p>
                  </div>
                )}
                
                {result && (
                  <div className="mt-4 p-4 border rounded-lg bg-secondary">
                    <div className="flex items-center gap-2 mb-2 text-secondary-foreground">
                      <Clock3 className="h-5 w-5" />
                      <h3 className="font-medium">Calculation Result</h3>
                    </div>
                    <p>
                      Recommended green light duration: 
                      <span className="font-bold ml-2 text-lg">
                        {result.greenLightDuration} seconds
                      </span>
                    </p>
                  </div>
                )}
              </form>
            </Form>
          </CardContent>
        </Card>
        
        <Card>
          <CardHeader>
            <CardTitle>Traffic Flow Analysis</CardTitle>
            <CardDescription>
              Visualizing the relationship between vehicle count and green light duration
            </CardDescription>
          </CardHeader>
          <CardContent>
            <ResponsiveContainer width="100%" height={300}>
              <LineChart
                data={chartData}
                margin={{ top: 5, right: 30, left: 20, bottom: 5 }}
              >
                <CartesianGrid strokeDasharray="3 3" />
                <XAxis 
                  dataKey="cars" 
                  label={{ value: 'Number of Vehicles', position: 'insideBottomRight', offset: -10 }} 
                />
                <YAxis 
                  label={{ value: 'Duration (seconds)', angle: -90, position: 'insideLeft' }} 
                />
                <Tooltip />
                <Legend verticalAlign="top" height={36} />
                <Line 
                  type="monotone" 
                  dataKey="duration" 
                  name="Green Light Duration" 
                  stroke="hsl(var(--chart-1))" 
                  activeDot={{ r: 8 }} 
                />
              </LineChart>
            </ResponsiveContainer>
            <p className="text-sm text-muted-foreground mt-4">
              This chart shows how green light duration changes based on traffic volume.
              Each point represents a calculation with the given number of vehicles.
            </p>
          </CardContent>
        </Card>
      </div>
    </div>
  );
}